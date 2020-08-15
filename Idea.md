Tapjoy problems:
- Uses hardcoded path in Assets folder in Dll
- adds directory to Plugins/Platform


1. can be ignored or decompile dll...
2. External plugin that

Platform Dependency Manager:
- Exposes API that:
	- Creates folders under Plugins/"Platform"
	- or deletes them when plugin is uninstalled

- Additionally it can create custom folder structure required by some plugins like Tapjoy

			-