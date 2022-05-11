dotnet publish -c Release
set /p AccessKeyId=<../../../../tokens/AccessKeyId
set /p SecretAccessKey=<../../../../tokens/SecretAccessKey
yc serverless function version create --function-name=no-cost-site --runtime dotnetcore31 --entrypoint NoCostSite.Api.EntryPoint --memory 128m --execution-timeout 3s --service-account-id aje4034bmbfmdstij739 --source-path ".\bin\Release\netcoreapp3.1\publish" --environment AccessKeyId=%AccessKeyId%,SecretAccessKey=%SecretAccessKey%