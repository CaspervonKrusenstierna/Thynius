
using ThyniusService;

string thyniusInstallationDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Thynius\\";
string thyniusDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Thynius\\";
ServerComms serverComms = new ServerComms("https://thynius.com", thyniusDataDir + "CookieData.txt");
SystemEventsManager systemEventsManager = new SystemEventsManager(thyniusInstallationDir, thyniusDataDir, serverComms);