# EmployeeDetails_CRUD_Operation
WPF APPLICATION CASE STUDY
In this WPF application we can do Get all the employee list , Export Details to CSV file and Delete the specific details  Operation for employee using ReSTFul Services.
Used ReST Api Service to do the above mention actions.
To authenticate Api used Token based authentication to connect.
All the layer has seperated to perform do their seperate responsibilites.
Api Layer to do api actions like Get , Delete and access the baseaddress , token and endpoints.
BaseAddress , TokenKey and EndPoints are handle in setting.xml file to avoid the hardcoded in class file.
To access the Api credentails separate Common class has function to get those details.
MSUnit test has also added in the application. 
Some of the unit test like api response and methods are getting the values properly or not in application.
Apart from this validation has added in button click Events.
