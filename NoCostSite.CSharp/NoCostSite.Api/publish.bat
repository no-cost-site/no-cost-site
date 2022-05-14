dotnet publish -c Release

set /p PublicBucketName=<../../../../settings/PublicBucketName
set /p PrivateBucketName=<../../../../settings/PrivateBucketName
set /p ObjectStorageAccessKeyId=<../../../../settings/ObjectStorageAccessKeyId
set /p ObjectStorageSecretAccessKey=<../../../../settings/ObjectStorageSecretAccessKey
set /p ObjectStorageServiceUrl=<../../../../settings/ObjectStorageServiceUrl
set /p ObjectStorageRegion=<../../../../settings/ObjectStorageRegion
set /p TokenIssuer=<../../../../settings/TokenIssuer
set /p TokenAudience=<../../../../settings/TokenAudience
set /p TokenSecureKey=<../../../../settings/TokenSecureKey
set /p TokenExpirationDays=<../../../../settings/TokenExpirationDays
set /p DataBaseSecureKey=<../../../../settings/DataBaseSecureKey
yc serverless function version create --function-name=no-cost-site --runtime dotnetcore31 --entrypoint NoCostSite.Api.EntryPoint --memory 128m --execution-timeout 3s --service-account-id aje4034bmbfmdstij739 --source-path ".\bin\Release\netcoreapp3.1\publish" --environment PublicBucketName=%PublicBucketName%,PrivateBucketName=%PrivateBucketName%,ObjectStorageAccessKeyId=%ObjectStorageAccessKeyId%,ObjectStorageSecretAccessKey=%ObjectStorageSecretAccessKey%,ObjectStorageServiceUrl=%ObjectStorageServiceUrl%,ObjectStorageRegion=%ObjectStorageRegion%,TokenIssuer=%TokenIssuer%,TokenAudience=%TokenAudience%,TokenSecureKey=%TokenSecureKey%,TokenExpirationDays=%TokenExpirationDays%,DataBaseSecureKey=%DataBaseSecureKey%