# PSCoreDataUploaderApplication
This application reads from raw CSV files, process the data and uploads those to a MySQL Database.

Add an ```App.config``` file in the ```PSCoreZte``` folder and edit the file like below:

```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="connectionString" connectionString="server=172.16.8.224;user=root;database=monitor;port=3306;password=;" />
  </connectionStrings>
    <appSettings>
      <add key="UploadToOracle" value="true"/>
      <add key="rootDrive" value="F:\"/>
      <add key="sendSms" value="false"/>
      <add key="receiverNo" value="01962400740,01939900253"/>
    </appSettings>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
    </startup>
</configuration>

```
