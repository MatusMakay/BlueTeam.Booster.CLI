## Initialization and Activation of Use Cases
Ensure connectivity via CyberOps WiFi or a VPN to interact with our application.

### Running the CLI Application
Use the following commands in PowerShell to clone and initialize the CLI application:

``` Powershell
git clone git@github.com:MatusMakay/BlueTeam.Booster.CLI.git
cd BlueTeam.Booster.CLI
cd Bc.CyberSec.Detection.Booster.CLI
```
Modify the `init-Windows.ps1` script to include the API key value, which is available in the digital version of my bachelor's thesis under `BP_MatusMakay/Autorizácia`.

``` Powershell
PowerShell -ExecutionPolicy Bypass -File .\init-Windows.ps1
```

Restart your computer or close the terminal to ensure that the environment variables are properly set.

Next, publish and execute the application:

``` Powershell
dotnet publish -c Release -o out
cd out
.\Bc.CyberSec.Detection.Booster.CLI.exe
```
After execution, the CLI will display a list of available command switches.

### Use Case Initialization
The Init directory contains initialization files necessary for the application. The supported file formats are CSV and XLSX. Review these templates to ensure the proper structure is maintained. The production build can be found in the ./out directory.

Initialize use cases by running:

``` Powershell
.\Bc.CyberSec.Detection.Booster.CLI.exe -i ./Init/UseCaseFinal.xlsx
10UC was created.
``` 
For a detailed output, execute with the -t switch:

``` Powershell
.\Bc.CyberSec.Detection.Booster.CLI.exe -i ./Init/UseCaseFinal.xlsx -t
UC1 was created. Details:
   - Name: CyberSec.Booster: ARP spoofing
   - Mitre Attack Id: T1557.002
   - Kibana Rule Id: ID
   - Mnemonics: SW_MATM-4, SW_DAI-4
...
UC10 was created. Details:
   - Name: CyberSec.Booster: Manipulation of the STP protocol
   - Mitre Attack Id: T1498.001
   - Kibana Rule Id: ID
   - Mnemonics: SPANTREE
```
### Use Case Activation
Ensure that the use cases are initialized as per the previous steps before activation. Activate individual or multiple use cases using:

``` Powershell
.\Bc.CyberSec.Detection.Booster.CLI.exe -a 1
UC1 was activated.
``` 
To activate multiple use cases simultaneously, use the range feature:

``` Powershell
.\Bc.CyberSec.Detection.Booster.CLI.exe -r 1..3,7..10 -a
UC1 was activated. 
UC2 was activated.
UC3 was activated.
UC7 was activated.
UC8 was activated.
UC9 was activated.
UC10 was activated.
``` 
Deactivating use cases follows the same procedure as activation.