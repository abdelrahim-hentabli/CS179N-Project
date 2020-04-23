# CS179N-Project
## Using Git/Github for Unity with our project
### Creating your Branch:
1. In the Github panel in Unity, switch to the "Branches" tab. 
2. Single-click on master and then press the "New Branch" button. 
3. Give your branch a name and press "create."

### Initializing your branch:
1. In the "Branches" tab, make sure your branch is selected. If it isn't, double click on your branch and it will prompt you if you want to switch to your branch. Click "switch".
2. Go to the "Changes" tab. Unity will tell you there if there are any changes to be commited.
3. Click on the "All" button to select all files needing to be changed.
4. At the bottom of the Github panel, write a commit summary/description and then press the "commit to \[your-branch-name]\" button.
5. Press the "Push" button at the top of the Github panel. If successful, Unity will tell you in a new window. To check, go to the repo on Github website and click on the "master" tab. Your branch should be there.

### I want to push changes to my branch:
1. In Unity, go to the "Changes" tab and select the files you want to commit.
2. At the bottom of the Github panel, write a commit summary/description and then press the "commit to \[your-branch-name]\" button.
3. Press the "Push" button at the top of the Github panel. If successful, Unity will tell you in a new window.

### I want to grab the latest build from master:
1. Open a new terminal window and navigate to our repo.
2. Switch to your branch with
`git checkout your-branch-name`.
3. Grab the latest changes with
`git pull origin master`.
4. Open Unity. Make sure your branch is seleced in the "Branches" tab. Push immediately to your branch by pressing the "Push" button.

### A feature is fully tested/completed and I want to merge my branch with master:
1. Open a new terminal window and navigate to our repo.
2. Switch to master with
`git checkout master`
3. Merge your branch with `git merge your-branch-name`.
4. Open Unity. Make sure master is seleced in the "Branches" tab. Push immediately to master by pressing the "Push" button.

### Hello, this is just a test message from Alex to see if I can push anything.
### My bad, I meant to push this message into the README from my branch.
