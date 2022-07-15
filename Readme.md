# Bygdrift.Tools.Log

Microsoft.Extensions.Logging.ILogger cannot hold inputs so it's possible to later ask what inputs have been send to ILogger. So that is the reason for this tiny library. 

## Example:

```c#
[TestMethod]
public void SimpleLogMessage()
{
    Mock<ILogger> loggerMock = new();

    var log = new Log(loggerMock.Object);
    log.LogInformation("Started");
    log.LogError("Error thrown: {A}", "An error");  //Messages can be parsed with curly brackets like in ILogger: loggerMock.Object.LogError("Error thrown: {A}", "An error");

    var logs = log.GetLogs();  //This is not possible with ILogger
    Assert.IsTrue(logs.Count() == 2);

    var errors = log.GetErrorsAndCriticals();  //This is not possible with ILogger
    Assert.IsTrue(errors.Count() == 1);
}
```
