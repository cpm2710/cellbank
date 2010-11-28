package com.turen.linklinklook;

import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Random;

import org.loon.framework.android.game.action.sprite.Picture;
import org.loon.framework.android.game.core.graphics.LImage;
import org.loon.framework.android.game.core.graphics.Screen;
import org.loon.framework.android.game.core.graphics.device.LGraphics;
import org.loon.framework.android.game.core.graphics.window.LButton;
import org.loon.framework.android.game.core.graphics.window.LMessage;
import org.loon.framework.android.game.utils.GraphicsUtils;

import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MotionEvent;

import com.turen.linklinklook.renren.FriendParser;
import com.turen.linklinklook.renren.RenRen;
import com.turen.linklinklook.renren.RenrenError;
import com.turen.linklinklook.renren.Util;

public class LLKScreen extends Screen {
	private RenRen renren;

	public RenRen getRenren() {
		return renren;
	}

	public void setRenren(RenRen renren) {
		this.renren = renren;
	}

	private Picture role;
	private LMessage mes;
	private Main main;
	private LButton button;
	private int xBound;
	private int yBound;
	private Grid nexts;

	private Grid nexte;
	private com.turen.linklinklook.Grid[][] grid;
	private int offsetX;
	private int offsetY;
	private boolean wingame;
	private LinkedList<Grid>[] path;
	private int pcount;
	private LevelInfo levelInfo;

	public LLKScreen(Main main) {
		this.main = main;
	}

	public void onLoad() {
		setBackground(Images.getInstance().getImage(8));
		// renren.authorize(new LoginDialogListener());
		initUI();
	}

	private boolean xdirect(Grid start, Grid end, LinkedList<Grid> path) {
		if (start.getYpos() != end.getYpos())
			return false;
		int direct = 1;
		if (start.getXpos() > end.getXpos()) {
			direct = -1;
		}
		path.clear();
		for (int x = start.getXpos() + direct; x != end.getXpos() && x < xBound
				&& x >= 0; x += direct) {
			if (grid[start.getYpos()][x].isVisible()) {
				return false;
			}
			path.add(grid[start.getYpos()][x]);
		}

		path.add(end);
		return true;
	}

	private boolean ydirect(Grid start, Grid end, LinkedList<Grid> path) {
		if (start.getXpos() != end.getXpos()) {
			return false;
		}
		int direct = 1;
		if (start.getYpos() > end.getYpos()) {
			direct = -1;
		}
		path.clear();
		for (int y = start.getYpos() + direct; y != end.getYpos() && y < yBound
				&& y >= 0; y += direct) {
			if (grid[y][start.getXpos()].isVisible()) {
				return false;
			}
			path.add(grid[y][start.getXpos()]);
		}

		path.add(end);
		return true;
	}

	private int findPath(Grid start, Grid end) {
		if (xdirect(start, end, path[0])) {
			return 1;
		}
		if (ydirect(start, end, path[0])) {
			return 1;
		}
		Grid xy = grid[start.getYpos()][end.getXpos()];
		if (!xy.isVisible() && xdirect(start, xy, path[0])
				&& ydirect(xy, end, path[1])) {
			return 2;
		}
		Grid yx = grid[end.getYpos()][start.getXpos()];
		if (!yx.isVisible() && ydirect(start, yx, path[0])
				&& xdirect(yx, end, path[1])) {
			return 2;
		}
		path[0].clear();
		for (int y = start.getYpos() - 1; y >= 0; y--) {
			xy = grid[y][start.getXpos()];
			yx = grid[y][end.getXpos()];
			if (xy.isVisible()) {
				break;
			}
			path[0].add(xy);
			if (!yx.isVisible() && xdirect(xy, yx, path[1])
					&& ydirect(yx, end, path[2])) {
				return 3;
			}
		}

		path[0].clear();
		for (int y = start.getYpos() + 1; y < yBound; y++) {
			xy = grid[y][start.getXpos()];
			yx = grid[y][end.getXpos()];
			if (xy.isVisible()) {
				break;
			}
			path[0].add(xy);
			if (!yx.isVisible() && xdirect(xy, yx, path[1])
					&& ydirect(yx, end, path[2])) {
				return 3;
			}
		}

		path[0].clear();
		for (int x = start.getXpos() - 1; x >= 0; x--) {
			yx = grid[start.getYpos()][x];
			xy = grid[end.getYpos()][x];
			if (yx.isVisible()) {
				break;
			}
			path[0].add(yx);
			if (!xy.isVisible() && ydirect(yx, xy, path[1])
					&& xdirect(xy, end, path[2])) {
				return 3;
			}
		}

		path[0].clear();
		for (int x = start.getXpos() + 1; x < xBound; x++) {
			yx = grid[start.getYpos()][x];
			xy = grid[end.getYpos()][x];
			if (yx.isVisible()) {
				break;
			}
			path[0].add(yx);
			if (!xy.isVisible() && ydirect(yx, xy, path[1])
					&& xdirect(xy, end, path[2])) {
				return 3;
			}
		}

		return 0;
	}

	private void shuffle(Grid array[], int count) {
		if (wingame) {
			return;
		}
		int number = 0;
		do {
			getSprites().setVisible(false);
			for (int i = 0; i < count; i++) {
				int j = (int) (Math.random() * (double) count);
				int k = (int) (Math.random() * (double) count);
				LImage temp = array[k].getImage();

				array[k].setImage(array[j].getImage());
				array[j].setImage(temp);
			}

			getSprites().setVisible(true);
			number++;
			if (number > 5) {
				wingame = true;
				break;
			}
		} while (!findPair());
	}

	private boolean findPair() {
		nexts = null;
		nexte = null;
		for (int sy = 1; sy < yBound - 1; sy++) {
			for (int sx = 1; sx < xBound - 1; sx++)
				if (grid[sy][sx].isVisible()) {
					for (int ey = sy; ey < yBound - 1; ey++) {
						for (int ex = 1; ex < xBound - 1; ex++)
							if (grid[ey][ex].isVisible()
									&& (ey != sy || ex != sx)
									&& grid[sy][sx].equals(grid[ey][ex])) {
								pcount = findPath(grid[sy][sx], grid[ey][ex]);
								if (pcount != 0) {
									nexts = grid[sy][sx];
									nexte = grid[ey][ex];
									return true;
								}
							}

					}

				}

		}

		return false;
	}

	private void initUI() {
		// ConnectButton button;
		levelInfo = new LevelInfo(5, 5);
		role = new Picture(Images.getInstance().getImage(0), 0, 0);
		button = new LButton("login", 0, 0, 100, 100) {
			public void doClick() {
				renren.authorize();
				main.setHeaderImages(FriendParser.parseHeaderLImages(renren));
				// main.getHeaderImages().put(key, value)
				xBound = levelInfo.getxBound() + 2;
				yBound = levelInfo.getyBound() + 2;

				grid = new Grid[yBound][xBound];

				int count = 0;
				Grid temp[] = new Grid[xBound * yBound];
				int sub = xBound;
				for (int y = 0; y < yBound; y++) {
					for (int x = 0; x < xBound; x++) {
						/*grid[y][x] = new Grid(null, x, y);

						LImage img = main.getHeaderImages().get(
								main.getHeaderUrls().get((count % sub)));
						;
						grid[y][x].setImage(img);
						grid[y][x].setBorder(3);
						int nx = offsetX + x * img.getWidth();
						int ny = offsetY + y * img.getHeight();

						grid[y][x].setLocation(nx, ny);
						temp[count] = grid[y][x];
						count++;*/

						//getSprites().add(grid[y][x]);
					}

				}
				// shuffle(temp, count);
			}
		};

		wingame = false;
		add(role);
		add(button);
	}

	@Override
	public void draw(LGraphics arg0) {
		// TODO Auto-generated method stub

	}

	@Override
	public void onTouch(float arg0, float arg1, MotionEvent arg2, int arg3,
			int arg4) {
		// TODO Auto-generated method stub

	}

	@Override
	public boolean onKeyDown(int arg0, KeyEvent arg1) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean onKeyUp(int arg0, KeyEvent arg1) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean onTouchDown(MotionEvent arg0) {
		// TODO Auto-generated method stub

		return false;
	}

	@Override
	public boolean onTouchMove(MotionEvent arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean onTouchUp(MotionEvent arg0) {
		// TODO Auto-generated method stub
		return false;
	}
}
