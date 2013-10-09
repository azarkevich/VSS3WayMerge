VSS3WayMerge
============

Tool for support merge two VSS databases with common ancestor.

============
VSS obviuously should not be used as VCS tool, but some unlucky peoples still use it. Although VSS internally supports branch/merge, yet another way can be used:

Say you have \\server\mainline\ VSS database. In time X you decide to branch. Then perform copy of \\server\mainline\ to, say, \\server\branch-1.0\, delete journal file of branch-1.0 and continue development in both 
branches.

After some time you get next picture:

(\\server\mainline) ------- >  \\server\mainline
 \
  \------------------------->  \\server\branch-1.0
  
In both VSS databases was applied some changes. Changes in \\server\branch-1.0 should be merged back into \\server\mainline

The first method is usual 2-way compare:
- get both VSS codebases side-by-side and then try merge differences from one VSS to another. This way is hard and error prone. This is because it is hard to say if new(more correct) changes was made on left or righ source codebase. This method acceptable for small codebases, when responsible for merge guy know all performed changes.

Second way used in all modern VCS tools: 3-way merge. We have 3 files:
1. base version. file which was in VSS database before create branch.
2. 'theirs' version - modified file in \\server\branch-1.0. Changes in this file should be applied to file in \\server\mainline 
3. 'mine' version - modified file in \\server\mainline VSS database. Changes in this file should be preserved during merge.

So, operation is simple: get difference between 'base' - 'theirs' and apply it to 'mine' file.

And this is work for VSS3WayMerge tool. Start it and select \\server\branch-1.0 as 'theirs' (it will be source for differences) and \\server\mainline as 'mine' (it will be merge destination).
Then detect VSS journal file and parse it. !! Journal file is crucial point. It should be enabled or recreated immediately after create branch. This file used for detect changes in VSS database and calculate base versions.

When changed files detected and loaded into 'Diff' pane, just select all them and perform 'Automatical merge' from context menu. All chnaged (and mergeable) files will be merged. In case of conflicts you can start compare tool and resolve them manually. 

That is what tool do.
