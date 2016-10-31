[![Check out this blog post.](http://robin.lankhorst.link/wp-content/uploads/2016/10/OsigInjector.png)](http://robin.lankhorst.link/?p=177)

An Android utility for injecting Oculus signature files into an APK, and resigning it.
## Usage
```
OsigInjector 0.1.0
Copyright (C) 2016 Robin Lankhorst
Usage: osiginject
The application requires JarSigner from JDK, and that the bin/ folder is in the
 PATH variable.

  -i, --input        Required. Input APK file
  -o, --output       Required. Target output APK path
  --keystore         Required. Path to the used key store
  -s, --storepass    Required. Text file containing the password for the
                     keystore
  --keyalias         Required. Key alias within the store.
  -k, --keypass      Required. Text file containing the password for the key
                     alias
  --btool            Required. Path to Android build tools
  --help             Display this help screen.
```

## A personal note
If some things are not working as intended, or you have any suggestions how I could improve this project, please let me know. I'm only human, could've missed something. 😄 
 
## Dependencies
### External
* APKtool, as `apktool.bat` in the working directory.
	* Get it [here](https://ibotpeaches.github.io/Apktool/).
* Java Development Kit, with `jarsigner` in system `PATH` variable (`bin` folder).
* Android Build Tools containing `zipalign`, also in `PATH`.

### Personal
* A keystore file, with access to the target alias and passwords.
* Text files containing the passwords for both the keystore as the key.
	* Done for the reason that escaped characters will be read correctly (had some issues in the console window).

## Build it yourself
Microsoft Visual Studio version capable of targeting .NET Framework 4.6.1 (lower versions should work just fine though). The project integrates a NuGet package for parsing command line entries, [CommandLineParser by Giacomo Stelluti Scala](https://www.nuget.org/packages/CommandLineParser/). Make sure NuGet fixes packages before building yourself a copy.

## Questions?
Reach me quickly using my [Twitter](http://twitter.com/robinlankhorst), or by commenting on [this blog post](http://robin.lankhorst.link/?p=177).
