## Conventions

### Issues
Formulate issue titles in the imperative. Keep them plain and simple.\
They should be in the following format:\
`[ISSUE_TYPE] ISSUE_TITLE` (e.g. **[Feature] Do this and that** or **[Bug] Fix this and that**)

Provide as much detail as possible in the descriptions.

### Pull requests
Keep one issue per pull request!

Squashing commits before the merge is a must!

Pull request titles should match their corresponding issue titles:\
`[ISSUE_TYPE] ISSUE_TITLE` (e.g. **[Feature] Do this and that** or **[Bug] Fix this and that**)

The description can be left empty.

### Commit messages
Squashed commit messages should be in the following format:\
`#ISSUE_NUMBER ISSUE_TITLE` (e.g. **#1 Do this and that** or **#2 Fix this and that**)

Additional message (if applicable) can be put in the 2nd line.

### Branches and tags
Feature and bugfix branches should preferably be named in the following format:\
`feature/ISSUE_NUMBER-ISSUE_TITLE` (e.g. **feature/1-Do-this-and-that** or **bugfix/2-Fix_this_and_that**)

Release branches and release tags should be named in the following format:\
`release/VERSION` (e.g. **release/1.0.0**).
