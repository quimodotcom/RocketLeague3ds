//
// CTR - Library : AppletPlugin
//

#include <stdio.h>
#include <string.h>

#include <nn/os.h>
#include <nn/erreula.h>
#include <nn/dbg.h>

#include "AppletPlugin.h"
#include "AppletHandler.h"	// This file is located in the folder "\Editor\Data\PlaybackEngines\N3DS\Includes", under the path you installed Unity into.

extern "C"
{

static char message[128];

#define ARRAY_SIZE(_a)	(sizeof(_a) / sizeof((_a)[0]))

void ShowAppletImpl ();

void ShowApplet (const char* m)
{
	strncpy(message, m, ARRAY_SIZE(message) - 1);

	N3dsAddAppletHandler(ShowAppletImpl);
}

void ShowAppletImpl ()
{
	N3dsRemoveAddAppletHandler(ShowAppletImpl);

	nn::applet::AppletWakeupState wakeUpState;

	nn::erreula::Parameter params;
	nn::erreula::InitializeConfig(&params.config);

	params.config.errorType = nn::erreula::ERROR_TYPE_ERROR_TEXT_WORD_WRAP;
	std::mbstowcs(params.config.errorText, message, ARRAY_SIZE(params.config.errorText) - 1);

	params.config.useLanguage = nn::erreula::USE_LANGUAGE_DEFAULT;
	params.config.upperScreenFlag = nn::erreula::UPPER_SCREEN_NORMAL;	// ignored
	params.config.homeButton = false;
	params.config.softwareReset = false;
	params.config.appJump = false;

	nn::erreula::StartErrEulaApplet(&wakeUpState, &params);
}

}
