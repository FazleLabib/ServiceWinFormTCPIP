# ServiceWinFormTCPIP

A simple C# project that establishes TCP/IP communication between a Windows Forms application and a worker service.

## Build and Run Instructions

### Windows Form Application

1. **Select Release Configuration:**
   - Set the solution configuration to `Release`.

2. **Build the Project:**
   - Right-click on the `WinFormsApp` project and select `Build`.

3. **Locate the Executable:**
   - The executable file will be located at:  
     `..\WinFormsApp\bin\Release\net8.0-windows`

4. **Run the Application:**
   - Run the application by executing the `.exe` file from the above location.

### Worker Service

1. **Build the Project:**
   - Right-click on the `WorkerService` project and select `Build`.

2. **Publish the Service:**
   - Right-click on the project again and select `Publish`.
   - In the publish window, click the `Publish` button.

3. **Locate the Published Service:**
   - The published service executable will be located at:  
     `..\workerservice\bin\Release\net8.0\publish\win-x64`

### Run the Worker Service

1. **Open PowerShell as Administrator:**
   - Open PowerShell with elevated privileges.

2. **Create the Service:**
   - Execute the following command to create the service:
     ```powershell
     sc.exe create <service-name> binpath= "<target-location>" start= auto obj= LocalSystem
     ```

3. **Start the Service:**
   - Start the service with the command:
     ```powershell
     sc.exe start <service-name>
     ```

4. **Stop the Service:**
   - Stop the service with the command:
     ```powershell
     sc.exe stop <service-name>
     ```
