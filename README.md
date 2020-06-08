# PSCoreDataUploaderApplication
This application reads from raw CSV files, process the data and uploads those to a MySQL Database.

Add an ```App.config``` file in the ```PSCoreZte``` folder and edit the file like below:

```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="connectionString" connectionString="server=XXX.XXX.XXX.XXX;user=user_name;database=db_name;port=port_number;password=password;"/>
  </connectionStrings>
    <appSettings>
      <add key="UploadToDB" value="true"/>
      <add key="rootDrive" value="F:\"/>
    </appSettings>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
    </startup>
</configuration>

```
