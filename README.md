# FlightControlWeb


An air control system, which monitor active flights and enable new flight plans to be entered.

The system will be in sync with other air control systems, so that each user can monitor the status of flights entered 
into our system as well as other flights entered into external air control systems.

### Objectives:

* Using **ASP.NET Core 3.1** to create a modern Web application.
* Creating **REST API** using WebAPI
* Client design based on **bootstrap** and **css** principles
* **JavaScript** client-side app based on **ES8** 
* **Unit tests** and Dependency Injection.

### API Routes:

   
   | ACTION |                       PATH                    | DESCRIPTION  |
   | :------: |:--------------------------------------------| :-----------------------------------------------------------------------------------------------------------------------------------------------------------|
   | GET    | /api/Flights?relative_to=<DATE_TIME>          |Returns an array containing the states of all active flights entered to the current server (is_external: false) relative to the time specified in the request|
   | GET    | /api/Flights?relative_to=<DATE_TIME>&sync_all |Same as above, returns all flights that the server can discover, both directly entered and external servers it is in sync with                               |
   |        |                                               |                                                                                                                                                             |
   | POST   | /api/FlightPlan                               |Adds a new flight plan. the flight plan is specified by an appropriate object in the body of the request                                                     |
   | GET    | /api/FlightPlan/{id}                          |Returns the flight plan with the particular ID                                                                                                               |
   | DELETE | /api/Flights/{id}                             |Deletes a flight with a specific ID previously entered to the current server                                                                                 |
   |        |                                               |                                                                                                                                                             |
   | GET    | /api/servers                                  |Returns list of external servers                                                                                                                             |
   | POST   | /api/servers                                  |Adds a new external server                                                                                                                                   |
   | DELETE | /api/servers/{id}                             |Deletes a server with a specific ID previously entered                                                                                                       |
