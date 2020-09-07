# LfsHelper

This will be a rather simple tool to surround LFS locking mecanism  

The goal is to make it as simple as possible for non programmer to use the lock/unlock of LFS.  
The idea comes from an experience I had during a gamejam where it rapidly became obvious that the lock/unlock command were night unusable for our artists.  

This tool will:  
1. Show a repo as a filetree
2. Allow for the locking of files with a simple button
3. Allow for the unlocking of files with a simple button
4. Show who has locked a file in the event that a lock is impossible
5. Update which files are locked every 1-2 minutes
    1. Show the time since the last update
6. Show which types of files are lockage
    1. Allow to edit this list inside the tool
