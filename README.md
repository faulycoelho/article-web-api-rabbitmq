```
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=guest rabbitmq:3-management
docker run -d --name meu-sql-server -p 1433:1433 -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=SuaSenhaSegura" -v /caminho/para/data:/var/opt/mssql mcr.microsoft.com/mssql/server:2019-latest
```
Create the containers above and check if the application can connect to them:
http://localhost:5101/health

Eg.:
```
{
  "status": "Healthy",
  "totalDuration": "00:00:00.2439938",
  "entries": {
    "rabbitmq-check": {
      "data": {},
      "duration": "00:00:00.0169928",
      "status": "Healthy",
      "tags": [
        "rabbitmq"
      ]
    },
    "sqlserver-check": {
      "data": {},
      "duration": "00:00:00.2411064",
      "status": "Healthy",
      "tags": [
        "sqlserver"
      ]
    }
  }
}
```
Create a new Payment:
![image](https://github.com/faulycoelho/article-web-api-rabbitmq/assets/37049426/60eb4748-0e96-4f57-869d-6acead16a7f1)


The Api Background Worker will process it:

![image](https://github.com/faulycoelho/article-web-api-rabbitmq/assets/37049426/c22c1d09-5a61-4ad9-858b-b9204560fc4b)


You can get more details in: https://medium.com/@faulybsb/net-8-web-app-web-api-and-rabbitmq-e0209f9b1691
