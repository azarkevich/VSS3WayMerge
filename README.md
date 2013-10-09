VSS3WayMerge
============

Tool for support merge two VSS databases with common ancestor.

Why and when this tool can be used
------------
VSS obviuously should not be used as VCS tool, but some unlucky peoples still use it.

Although VSS internally supports branch/merge, yet another way can be used:

Say you have **\\\\server\mainline\** VSS database. In time X you decide to branch. Then perform copy of **\\\\server\mainline\** to, say, **\\\\server\branch-1.0\**, delete journal file of branch-1.0 and continue development in both 
branches.

After some time you get next picture:

<pre>
  (\\server\mainline) ------- >  \\server\mainline
   \
    \------------------------->  \\server\branch-1.0
</pre>

In both VSS databases was applied some changes. Changes in **\\\\server\branch-1.0\** should be merged back into **\\\\server\mainline\**

The first method is usual 2-way compare:
get both VSS codebases side-by-side and then try merge differences from one VSS to another. This way is hard and error prone. This is because it is hard to say if new(more correct) changes was made on left or righ source codebase. This method acceptable for small codebases, when responsible for merge guy know all performed changes.

Second way used in all modern VCS tools: 3-way merge. We have 3 files:

1. base version. file which was in VSS database before create branch.

2. 'theirs' version - modified file in **\\\\server\branch-1.0\**. Changes in this file should be applied to file in **\\\\server\mainline\** 

3. 'mine' version - modified file in **\\\\server\mainline\** VSS database. Changes in this file should be preserved during merge.

So, operation is simple: get difference between 'base' - 'theirs' and apply it to 'mine' file.

And this is work for VSS3WayMerge tool. Start it and select **\\\\server\branch-1.0\** as 'theirs' (it will be source for differences) and **\\\\server\mainline\** as 'mine' (it will be merge destination).
Then detect VSS journal file and parse it. !! Journal file is crucial point. It should be enabled or recreated immediately after create branch. This file used for detect changes in VSS database and calculate base versions.

When changed files detected and loaded into 'Diff' pane, just select all them and perform 'Automatical merge' from context menu. All chnaged (and mergeable) files will be merged. In case of conflicts you can start compare tool and resolve them manually. 

That is what tool do.

More details
--------------

1. Mine - destination vss database. Usually this is trunk or mainline. Branch changes should be merged in to this codebase. Theirs - VSS database which was branched from mainline and should be deleted after merge.
2. Tool require journal files only from 'theirs' database. This file parsed and appeared as easy editable list in text box. This 'preparsed' text can be edited. It's format: <file spec><tab><base version>[<tab><error>]. Files with optional 'error' string unable to merge. It is limitation of merge tool - moved, deleted and restored, archived, reverted, renamed, shared, branched files reckon as non-mergeable and should be merged manually.
3. Select merge destination: 'VSS Connect' - destination files in 'mine' database will be checked out and edited by tool. If you have local modifications, they will be overwritten without confirmation! This is more conveniet way - after merge you should only fin all checkedout files and checkin them. Second choice - 'detached'. Specify directory where merged files will be stored (hierarchy will be preserved). After merge you should upload all files to 'mine' VSS. Merge not required - it is already done. But here is danger also - if someone edit file in 'mine' after it was merged, then simple upload will overwrite this changes. Deny changes in both VSS databases for merge time.
4. Preparsed text should be parsed to list of changes. This list will be displayd on next tab.

Diff tab conatins list of changed files. Grayed files - unmergeable files and present for remnd you about manual merge. In diff tab can be performed next actions:

1. Main action - automatic merge. Select files and perform automatic merge. Unmergeable files will be ignored. Mergeable - get from VSS and merged with subversion merge algorithm (used SharpSvn assembly for this). Destination files will be checkedout if need. Conflicted and erroneous files marked in color.
2. If you select single file, in menu appered '3-Way diff' item (if file was not merged) or '3-way merge' if file was merged. First allow see potential merge result, but here no merge destination. Secodn allow resolve conflicts, ensure and correct successfully merged files (merge destination present).
3. Also here is other useful diffs


 - Theirs diff as patch - build patch file with theirs changes in selected files. Useful when some files in mine was moved to, say, Subversion. Patch file build with Subversion.
 - Theirs diff - difference produces in theirs version (base - theirs).
 - Mine diff - difference produces in mine version (base - mine).
 - Heads diff - compare lates version of files on both sides. This is what you see if try 2-way merge
 - Bases diff - get base version of file from both VSS and compare. This files should be identical. Otherwise here is somwhere error in base version detection. This merge tool always check both base versions and warn you if they different, so you can do something.
 
 
Some diffs allow compare many files at once. If 3 files selected, then they are joined into single file (with paddings between). This 'megafile' then comapred with 'megafile' from other side. This option allow to quick preview changes in many files at once. Say you suspect, that all files in this directory contains only unimportant changes which should be ignored. Then have a look at multi diff and ensure here is no significant changes. 

4. Merged diff - this is difference intent for apply to mine VSS. Good idea - check what will be checked in. Also this action can be performed from source safe application, as 'Difference' on cehckedout file
5. Resolve as mine/theirs - get mine/theirs version as result of merge. When you get 'mine' version, then file not changed at all. If you select 'theirs' version, then latest version of file from branch will be used.
6. Reset merged - remove merge destination/undo checkout. Then you can try merge again.
7. Remove from list - useful for merge by small sets of changes. When you finish merge with part of changes (say one directory), you can remove this changes from list and start next chunk of work.
8. Filters submenu allow filter files by filename (regex) or by selection. Useful for select and remove non-mergeable changes (dll, exe ...) or select set of files for work.
9. More submenu contains stats calculator and allow copy 'preparsed' text to clipboard. You can save this text somwhere and then continue work from this state, remove merged lines when they complete.
