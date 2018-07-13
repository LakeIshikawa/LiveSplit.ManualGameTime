# LiveSplit.RTA-TB
This is a LiveSplit component that can be used for games that feature time bonuses that are based on in-game time.

Last version: https://github.com/LakeIshikawa/LiveSplit.RTA-TB/raw/master/Components/LiveSplit.RTA-TB.dll

This component makes it possible to have both an IGT column and an RTA-TB column at the same time.
This component is mainly thought for use with Sonic 2.

This component is largely based on Sophira's similar plugin:
https://github.com/Sophira/LiveSplit.RealTimeMinusBonuses


## Usage
- Make sure you have one RTA splits column and one IGT splits column in your layout
- Set your comparsion method to RTA
- Set the correct data for the time bonus points and times in the component's settings
- At the end of each level, enter the IGT time in the input field and press ENTER at the time of the RTA split
- The timer will pause for the exact length of the time bonus, based on the data set in the component settings

## Special characters
The component supports 2 type of special characters:
- Enter 'c' after the time input to make the timer pause for an additional amount of time that's equal to the length of a continue
- Enter '-' after the time input for levels that do not have a time bonus

## Deaths
The parser supports multiple time inputs in the form t1+t2...+tn.
If you die, input the difference in time between the death time and the respawn, then input the final time of the level.
Only the final input will determine the time bonus.

## Final split
Since it might be hard to input the IGT time for the last split fast enough (for sonic 2, anyway), it is possible to 
pause the timer with the normal pause hotkey, and then input the time and split while the timer is paused.
The plugin will automatically unpause the timer and end the run.

## Undo a split
Unfortunately, due to a LiveSplit technical limitation, it's impossible to set an IGT split while skipping an RTA split.
This means that if you mess up a split you have to skip it entirely, in both RTA and IGT. 
You might want to fix the split after the run in this case.

## Examples
Finishing Emerald Hill 1 with a 21:

```21```

Dying at 0:45, respawning at 0:40 and finishing the level with 1:01 (no continue):

```5+61```

Dying at 0:45, respawning at 0:40 and finishing the level with 1:01 (with a continue):

```5+61c```

Finishing Wing Fortress in 1:44:

```104-```

Dying in Wing Fortress at 0:05, then finishing off in 1:44:

```5+104-```


## Limitations
- It's currently not possible to have a value for IGT and a skip for RTA in the same split
- Component settings are saved with the layout
