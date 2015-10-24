cf push pcf-contoso-mysql-dotnet -s windows2012R2 --no-start -b  https://github.com/cloudfoundry/binary-buildpack.git -m 256M
cf enable-diego pcf-contoso-mysql-dotnet
cf set-health-check pcf-contoso-mysql-dotnet none
cf start pcf-contoso-mysql-dotnet