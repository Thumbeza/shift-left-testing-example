# overview
The purpose of this prrojeect is to demonstrate how software engineering teams can implement a shift-left testing approach to ensure effective and early testing for their software. The main of shift-left testing is to identify and resolve bugs as early as possible in the SDLC, hence its core implementation is about shifting testing activities to earlier stages of the ci/cd pipeline, thus enabling an early feedback loop to software quality. Below are the details on how to implement and enforce this process:

# Application in Test
* **We.Sell.Bread.Core** - This is the core project where all the common elements of the application will reside. This speaks to interfaces, exceptions, DTOs and base functionality to be utalised by the API and Infrastructure projects.
* **We.Sell.Bread.infrastructure** - This project is responsible for processing database transactions. It houses the implementation of the repository pattern and managing the BD context.
* **We.Sell.Bread.API** - This is an api project for selling bread business case. All the business logic (services and facade) and api endpoints (controllers) that exposes functionality to the users will reside within this project.
* **We.Sell.Bread.API.Unit.Tests** - This is the unit test project that the software engineers must use to test the smallest modules of code, and ensure that they work as expected at the smallest unit.
* **We.Sell.Bread.API.Integration.Tests** - This project houses both the tests for different api endpoint calls and the integration between the different api components.

# Branching Strategy
For the purpose of this demonstration, a trunk based branching strategy will be used. The Main (or trunk) branch is always the main source of truth for the team. Software Engineers will create short lived feature or test branches from Main, implement the relevant changes and create a pull request to merge back to Main on frequent intervals. To support this branching strategy, planning must ensure that work is broken down into smaller features to allow individual engineers to work on those independantly and merge changes to Main frequently. For more details on Trunk Based Development, please see: https://www.atlassian.com/continuous-delivery/continuous-integration/trunk-based-development

example branches:
- feat/[issue-number]-adding-a-new-api-enpoint
- test/[same-as-the-above-issue-number]-adding-api-tests-for-the-above-issue
- release/R[release-number]

# Testing Process

## Unit and Integration Tests
- The testing model being followed is the 'Testing Throphy' which suggests that the bulk of the testing effort should covered by integration and unit tests.
- As such, for our API functionality testing, unit and integration tests will share the load to ensure conformance to requirements.
- In the shift-left testing approach, these tests are regarded as pre-deployment tests. Therefore, they will be executed as part of the build step in the ci pipeline to ensure an early feedback loop and also to prevent functional regressions as changes are being added.
- Unit and integration tests also serves as regression tests, a quick one at that, thus negating the need to run days or week long regression testing during releases.
- Code coverage will also be tracked between unit and integrration tests and total of 80% coverage will be accepted as the benchamrk to ensure continious effort for automated testing.

## Pull Requests
- A very stong pr policy must be in place to enforce the shift-left testing approach. 
- The testing process policy is as follows:
    - Once the Software Engineer/Developer has completed making changes to the feauture and have completed dev testing (manual and unit testing), they will raise a pull request to merge changes into the main branch.
    - The build step including unit and integration tests must execute successfully to ensure that there is no functional regression.
    - The application in test must be deployed to the test environment and all post deployment tests (PDTs) (system, performance and security) must execute with no errors.
    - Test Engineers must perform manual testing on the test environment for the new changes.
    - Test Engineers must create a test branch based on the feature branch and add all the relevant automated (integration, performane and system) tests regarding the functionality in question.  
    - Another test pull request must be raised by a test engineer to ensure all newly added tests execute succeessfully with no errors and that they now form part of the regression pack for future pull requests.
    - If any of the above test steps fail, bugs must be raised against the feature pull request, typically as comments, and the pull request must not be merged until all comments have been resolved.
    - If all the above checks succeed, an approval from a test engineer must be added for software quality sign off and another approval from a software engineer must be added for code quality sign off.
    - Subsequently, the two pull requests can be merged.

NB: As far as the CI is concerned, the pull requeest process starts from the build step and and ends on the test environment PDTs.
![pull_request](https://github.com/Thumbeza/shift-left-testing-example/assets/115139003/fedff177-a80f-4d27-827b-089991dbf3d4)


## Releases
- The release process is very similar to the pull request process with the following additions and exclusions:
    - Before kicking off the build process, a release branch must be created from the main branch and the a new build must be triggered for the newly created branch.
    - No new changes and automated tests will be added at this point, the regression pack (unit, integration and PDTs) should cover all the functionality to be released.
    - If the build step, test deployment steps, and an optional sanity checks succeed, the test engineer must approve the release for staging (in some cases, UAT) and production.
    - Staging or UAT deployment must also have automated PDTs to ensure the quality integrety in that environment. Any final sanity checks and/or user acceptence tests can be executed at this level..
    - Once the production sign-off is recieved, the deployment to production can commence.

# Missing Parts Explained
- something

# Some of the Benefits of Shift-Left-Testing
* **Extensive and early testing:** With the pr policy that enforces 80% unit test coverage and that all PDTs (e2e, security, performance) must pass before merging the changes to the Main branch, this allows for thourough testing at the early stages of the SDLC process. Resolving pr comments and fixing any issues at this level is much more cheaper and safier.
* **High confidence in code quality at all times:** The above testing approach creates a lot of confidence in the quality of the software being produced.
* **Quicker, modular and structured release process:** The confidence in code quality allows us to cut release branches from the Main branch at any point in time should it makes sense, in smaller, frequent increments. At this point, very less, to no testing is required, therefore enabling a very quick, automated release process. In my experience with this process, I have managed to do 3 production releases in a single day and that is certainly not the limit. Compared to typical days or week releases, the benefit of this process is simply invaluable.
* **Continious testing and moitoring:** The process is setup such that testing happens at all levels the build stage up until production to allow for continious feedback on the state of quality. Furthermore, creating Dashboards to measure the statee of Quality in the project is much more easy with this automated testing process. Some of the Quality Matrics you can measure are, code coverage per repo, bug density, release velocity, test veleocity, production health etc.
* **More time for more QA work and innovation:** Because the majority of the testing and release process is now automated, QA Engineers can take on multiple pieces of work at the same time and rely on the pipelines to accurately execute the testing activities. This also means that there is more time to investigate further process improvements and better ways of working.

