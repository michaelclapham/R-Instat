; Script generated by the Inno Script Studio Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{4263A8D4-1068-45F9-A542-C340C4A22E8B}
AppName=R-Instat
#define AppVerName "0.6.0"
AppVersion={#AppVerName}
AppPublisher=African Maths Institute
AppPublisherURL=http://r-instat.org/
AppSupportURL=http://r-instat.org/
AppUpdatesURL=http://r-instat.org/
DefaultDirName={autopf}\R-Instat\{#AppVerName}
DefaultGroupName=R-Instat
AllowNoIcons=yes
OutputBaseFilename=R-Instat_{#AppVerName}_Installer_64
SetupIconFile=..\instat\Resources\rinstat_icon_Hih_icon.ico
Compression=lzma
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}";
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "..\instat\bin\x64\Release\instat.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\instat\bin\x64\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\R-Instat"; Filename: "{app}\instat.exe"
Name: "{group}\{cm:UninstallProgram,R-Instat}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\R-Instat"; Filename: "{app}\instat.exe"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\R-Instat"; Filename: "{app}\instat.exe"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\instat.exe"; Description: "{cm:LaunchProgram,R-Instat}"; Flags: nowait postinstall skipifsilent
