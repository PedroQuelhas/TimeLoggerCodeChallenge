To generate a new version of the code, update the "PeriwinkleApi.yml" with the latest definition and run "CodeGen.bat" to update the code gen files


Use this manual command to bootstrap the project startup files if needed.
openapi-generator-cli generate -g aspnetcore -o out4 -i .\test.yml --additional-properties=aspnetCoreVersion=6.0,operationIsAsync=true,packageName=RetailManagement,packageTitle=Api,useDateTimeOffset=true,operationResultTask=true,pocoModels=true,buildTarget=library,classModifier=abstract,isLibrary=true