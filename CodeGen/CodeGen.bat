@echo off
call openapi-generator-cli generate -g aspnetcore -o code -i .\timelogger_api.yaml --additional-properties=aspnetCoreVersion=3.1,operationIsAsync=true,packageName=ServerApi.CodeGen,packageTitle=Api,useDateTimeOffset=true,operationResultTask=true,pocoModels=true,buildTarget=library,classModifier=abstract,isLibrary=true
rmdir /S /Q ..\ServerApi.CodeGen\
echo "Copy"
xcopy /Y /E .\code\src\ServerApi.CodeGen\*.* ..\ServerApi.CodeGen\
echo "Delete"
rmdir /S /Q .\code
pause