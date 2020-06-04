# EWS_Basic
Learn to call Exchange Web Service (EWS) FindItems method in a quick easy session. The solution contains two projects:

1. Website - Which had two simple forms, one to accept exchange server credential and URL from user along with the filter criteria, date-range and subject pattern, to search your mailbox. When user clicks on Search button, entire Inbox (It is limited to inbox for now) is searched to find the matching criteria. But, only top 10 recent emails are shown to the user in next form.

2. Web API - This API serves as the bridge between user interface and exchange server. It receives input from above website, and uses the input to connect to Exchange Web Service (EWS) and run the filter options on all emails. Top 10 results with very limited data is returned to the user interface.

Pre-requisites:
1. Exchange server credentials
2. URL for Exchange Web Services
3. Visual Studio 2017/19

Steps to configure code on your Local:
1. Download code on your local machine.
2. Open SearchEmail.sln file which will load both projects in visual studio
3. Compile code and fix issues if any.
4. Run Web API code and copy the URL (e.g. https://localhost:44391/api/)
5. Paste the URL by replacing URL in SearchEmail\appsettings.json file at "API": {"BaseAddress": "https://localhost:44391/api/"}
6. Note down your exchange server credentials and EWS URL.
7. Run both user interface website and Web API together
8. Provide input and test results.
