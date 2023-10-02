# overview
The purpose of this prrojeect is to demonstrate how software engineering teams can implement a shift-left testing approach to ensure effective and early testing for their software. Below are the details on how to implement and enforce this process:

# Projects
* Shift.Left.Testing.Poc - This is a simple api project for performing mathematical functions. All the business logic to be tested will reside within this project.
* Shift.Left.Testing.Poc.Tests - This is the unit test project that the software engineers must use to test the smallest modules of code, and ensure that they work as expected at the smalles unit.
* Shift.Left.Testing.Poc.e2e.Tests - This is the black box testing project for ensuring that the api works end to end as expected. The QA Engineers must use this project to add any relavent (black box) tests for any code changes that warrants it. Tests in this project should be added (if applicable) as part of the pr process before the pr can be approved and merged. This ensures that only code that has been extensively tested is merged to the main branch.

# Branching Strategy
For the purpose of this demonstration, a trunk based branching strategy will be used. The Main (or trunk) branch is always the main source of truth for the team. Software Engineers will create short lived feature or test branches from Main, implement the relevant changes and create a pull request to merge back to Main on frequent intervals. To support this branching strategy, planning must ensure that work is broken down into smaller features to allow individual engineers to work on those independantly and merge changes to Main frequently. For more details on Trunk Based Development, please see: https://www.atlassian.com/continuous-delivery/continuous-integration/trunk-based-development

example branches:
- feat/[issue-number]-adding-a-new-api-enpoint
- test/[same-as-the-above-issue-number]-adding-api-tests-for-the-above-issue

# Testing Process

## Unit Tests and Code Coverage

## Pull Requests

## Releases

# Missing Parts Explained

# Benefits of Shift-Left-Testing
* **Extensive and early testing:** With the pr policy that enforces 80% unit test coverage and that all PDTs (e2e, security, performance) must pass before merging the changes to the Main branch, this allows for thourough testing at the early stages of the SDLC process. Resolving pr comments and fixing any issues at this level is much more cheaper and safier.
* **High confidence in code quality at all times:** The above testing approach creates a lot of confidence in the quality of the software being produced.
* **Quicker, modular and structured release process:** The confidence in code quality allows us to cut release branches from the Main branch at any point in time should it makes sense, in smaller, frequent increments. At this point, very less, to no testing is required, therefore enabling a very quick, automated release process. In my experience with this process, I have managed to do 3 production releases in a single day and that is certainly not the limit. Compared to typical days or week releases, the benefit of this process is simply invaluable.
* **Continious testing and moitoring:** The process is setup such that testing happens at all levels the build stage up until production to allow for continious feedback on the state of quality. Furthermore, creating Dashboards to measure the statee of Quality in the project is much more easy with this automated testing process. Some of the Quality Matrics you can measure are, code coverage per repo, bug density, release velocity, test veleocity, production health etc.
* **More time for more QA work and innovation:** Because the majority of the testing and release process is now automated, QA Engineers can take on multiple pieces of work at the same time and rely on the pipelines to accurately execute the testing activities. This also means that there is more time to investigate further process improvements and better ways of working.

