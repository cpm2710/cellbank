package com.turen.linklinklook;

import org.loon.framework.android.game.action.sprite.Animation;
import org.loon.framework.android.game.action.sprite.Picture;
import org.loon.framework.android.game.core.graphics.LImage;
import org.loon.framework.android.game.core.graphics.device.LGraphics;



/**
 * Copyright 2008 - 2010
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 * 
 * @project loonframework
 * @author chenpeng
 * @email：ceponline@yahoo.com.cn
 * @version 0.1
 */
public class Grid extends Picture {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	private Animation animation;

	private int type, xpos, ypos;
	private String headerUrl;
	private String friendName;
	public Grid(LImage img) {
		super(img,0,0);
	}

	public Grid(LImage img,int x, int y) {
		super(img,x,y);
		xpos = x;
		ypos = y;
	}

	public int getXpos() {
		return xpos;
	}

	public int getYpos() {
		return ypos;
	}

	public boolean isPassable() {
		return !isVisible();
	}

	public void createUI(LGraphics g) {
		super.createUI(g);
		if (animation == null) {
			return;
		}
		if (type == 0 || type == 2) {
			LImage img = animation.getSpriteImage().getImage();
			if (img != null) {
				g.drawImage(img, x() + (getWidth() - img.getWidth()) / 2,
						y() + (getHeight() - img.getHeight()) / 2);
			}
		}
	}

	public void update(long t) {
		super.update(t);
		if (animation != null) {
			animation.update(t);
		}
	}

	public void setBorder(int type) {
		this.type = type;
		switch (type) {
		case 0:
			animation = Animation.getDefaultAnimation("res/s.png", 3, 48, 48,
					100);
			break;
		case 2:
			animation = Animation
					.getDefaultAnimation("res/s1.png", 48, 48, 100);
			break;
		default:
			break;
		}
	}

	public void setFriendName(String friendName) {
		this.friendName = friendName;
	}

	public String getFriendName() {
		return friendName;
	}

	public void setHeaderUrl(String headerUrl) {
		this.headerUrl = headerUrl;
	}

	public String getHeaderUrl() {
		return headerUrl;
	}

}
