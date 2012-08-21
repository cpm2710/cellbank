#include <windows.h>
#include <objbase.h>
#include <uxtheme.h>
#include <duser.h>
#include <duserctrl.h>
#include <directui.h>

using namespace DirectUI;

NativeHWNDHost *pnhh;

void InitDUI()
{
	InitProcess(DUI_VERSION);
	InitThread();
}

void UninitDUI()
{
	UnInitThread();
	UnInitProcess();
}

void CreateAppUI()
{
	HRESULT hr = S_OK;

	hr = NativeHWNDHost::Create(
		L"Hello DUI!", // title
		NULL, // parent
		NULL, // icon
		CW_USEDEFAULT, CW_USEDEFAULT, // dx,dy, 
		CW_USEDEFAULT, CW_USEDEFAULT, // cx,cy CW_USEDEFAULT are valid for size 
		WS_EX_CLIENTEDGE, WS_OVERLAPPEDWINDOW, // iExStyle - extended style, iStyle - window style,
		NHHO_DeleteOnHWNDDestroy|HHC_SyncPaint, // NHHO_HostControlsSize|NHHO_ScreenCenter supported
		&pnhh // reference to pointer for host window
		);
	DWORD dwDeferCookie;
	// map an element to the host window
	Element* pHostElement;
	HWNDElement::Create(pnhh->GetHWND(), 
		false, // doublebuffer
		0, // nCreate
		0, // parent
		&dwDeferCookie, // defer cookie
		&pHostElement // output element
		);

	// now host the element
	pnhh->Host(pHostElement);

	// Set the size and force the host element to appear
	pHostElement->SetVisible(true);

	pHostElement->SetContentString(L"Hello DUI!");

	pHostElement->EndDefer(dwDeferCookie);

	pnhh->ShowWindow();
}

STDAPI_(int) wWinMain(HINSTANCE hInstance, 
                HINSTANCE, LPWSTR, int nShowCmd)
{
	HRESULT hr = 0;
	hr = CoInitialize(NULL);

	InitDUI();

	CreateAppUI();

	//Run DUI message pump
	StartMessagePump();

	UninitDUI();

	CoUninitialize();
	return 0;
}
