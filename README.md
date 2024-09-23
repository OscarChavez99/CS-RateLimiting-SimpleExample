# CS-RateLimiting-SimpleExample
This project is an easy-to-understand example of how to use rate limiters in a Minimal API project in C#

## How It Works
1. Open the project in Visual Studio and run it
2. In Swagger, open the first endpoint called "TwoTimesPerIp
<br> ![image](https://github.com/user-attachments/assets/5dd165fe-344a-4068-a8da-36a7f1fbbfd7) <br>
3. Execute it, and on the third attempt, you will see that it returns a 429 error, which means "too many requests."
<br> ![image](https://github.com/user-attachments/assets/cbb8cdc3-a6e0-4678-aa97-035257b6f0fc)
 <br
5. Now, try the other endpoint called "ThreeTimesPerMinute," and you will see that on the fourth attempt within the same minute, it will not respond.
You will need to wait for a minute to retry
<br> ![image](https://github.com/user-attachments/assets/c0bb4dbc-bbf2-4335-be20-1864713c1da2)
