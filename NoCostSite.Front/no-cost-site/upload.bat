set /p Endpoint=<../../../../settings/Endpoint
set /p PublicBucketName=<../../../../settings/PublicBucketName
aws --endpoint-url=%Endpoint%/ s3 cp --recursive build/ s3://%PublicBucketName%/
