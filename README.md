# RivFox ©2024 License: MIT license
Microsoft Translator is not necessarily 100% accurate <br>
It's a fast, convenient, and simple HTTP request toolkit <br>
We believe that POST requests should be used when sending large amounts of data, so we did not encapsulate the body for GET requests
We have tested that the cookies you set will also be carried when the cookie returns. We are working on a solution for this proble

# Methods available
PostRequest_Json <br>
GETRequest_Json <br>
**There will be no conversion of the return value, and the body needs to handle the format by itself**
PostRequest <br>
GETRequest

# Argument:
**PostRequest_ReturnJson <br>**
*Necessary <br>*
url: Request URL <br>
*Optional <br>*
bodys: Request body <br>
headers: Request headers <br>
cookie: Request Cookie <br>
*Status check <br>*
online_bool(false):Network status <br>
status_bool(false): Request status/code <br>
time_bool(false): Request time <br>
header_bool(false): Response headers <br>
cookie_bool(false): Response cookie

**PostRequest: <br>**
*Necessary <br>*
url: Request URL <br>
*Optional <br>*
bodys: Request body <br>
headers: Request headers <br>
cookie: Request Cookie <br>
*Status check <br>*
online_bool(false):Network status <br>
status_bool(false): Request status/code <br>
time_bool(false): Request time <br>
header_bool(false): Response headers <br>
cookie_bool(false): Response cookie

**GETRequest_ReturnJson <br>**
*Necessary <br>*
url: Request URL <br>
*Optional <br>*
headers: Request headers <br>
cookie: Request Cookie <br>
*Status check <br>*
online_bool(false): Network status <br>
status_bool(false): Request status/code <br>
time_bool(false): Request time <br>
header_bool(false): Response headers <br>
cookie_bool(false): Response cookie

**GETRequest: <br>**
*Necessary <br>*
url: Request URL <br>
*Optional <br>*
headers: Request headers <br>
cookie: Request Cookie <br>
*Status check <br>*
online_bool(false): Network status <br>
status_bool(false): Request status/code <br>
time_bool(false): Request time <br>
header_bool(false): Response headers <br>
cookie_bool(false): Response cookie