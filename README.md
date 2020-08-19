### Dependency Linker
This package exists because of the upm package wraper problem.
There are many unity sdk's that don't support UPM thus they live in the project Assets folder and polute the folder structure with files that the developer doesnt want or shoulnt look at. If developer wants to remove such SDK from the project it is easy for him to miss some of the files/folder thus in time making the project more and more "dirty".
Making a custom UPM package that wraps this SDK is a option but sadly some of the SDK providers make use of hardcoded paths and DLL's making it hard to compleatly encapsulate the package.

Another type of the problematic SDK is the one that makes use of the mobile platform specific files.
For Android plugins it is common to add some Android project specific files that are required to exists in Asset/Plugins/Android/SDKFolder path.

This projects goal is to make such unbehavy SDK more reasonable to wrap inside a UPM package.

The solution in quite simple.
1. When wrapping the SDK put all of its "Asset" directory dependencies into one folder with xml manifest.
2. The xml contains the information about files and directories that are utilized by the package.
3. Linker then resolves those dependencies when project changes like so:
Resolver checks the if any of the packages in project contains the special xml.
If so ir reads is and imports those dependencies to the project Assets directory.
when it deos that it also remembers the package-dependdency paths connection so if you ever want to remove the package that contains dependencies in Assets folder it prompts you if you also want to remove x and y folders.

Because resolver stores the package:dependencies data you can also take a look at it anytime.

TODO:
* ~~Design manifest structure~~
* ~~Find manifest files in the project~~
* Manifest Parser
* Dependency resolver

QoL:
* When resolver sees that it has links from package that no longer exists in the project it asks the user if he wants to move them to trash.
* User can Manually mark folders as package dependencies that doesnt implement Dependency Links.
