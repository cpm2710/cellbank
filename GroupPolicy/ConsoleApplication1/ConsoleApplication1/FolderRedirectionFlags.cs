using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [Flags]
    public enum FlagBits
    {
        MoveContents = 0x00000001,
        FollowParentFolder = 0x00000002,
        RedirectionNotSpecified = 0x00000004,
        ExclusiveAccess = 0x00000010,
        RelocateOnMove = 0x00000020,
        CheckOwnership = 0x00000200,
        DoNotInheritFlags = 0x00000800,
        RedirectToFullPath = 0x00001000,
        RedirectToLocal = 0x00002000,
        ExcludeKnownSubFolders = 0x00004000,
        ApplyToDownlevel = 0x00008000
    }

    public class Flags
    {
        private int flagValue;

        public Flags(int value)
        {
            this.FlagValue = value | (int)FlagBits.RedirectToFullPath;
            // Must be always RedirectToFullPath for current spec
        }

        public bool MoveContents
        {
            get { return (this.FlagValue & (int)FlagBits.MoveContents) != 0; }
            set
            {
                if (value == true)
                {
                    this.FlagValue |= (int)FlagBits.MoveContents;
                }
                else
                {
                    this.FlagValue = (int)FlagBits.RedirectionNotSpecified;
                }
            }
        }

        public bool ExclusiveAccess
        {
            get { return (this.FlagValue & (int)FlagBits.ExclusiveAccess) != 0; }
            set
            {
                if (value == true)
                {
                    this.FlagValue |= (int)FlagBits.ExclusiveAccess;
                }
                else
                {
                    this.FlagValue &= (int)~(FlagBits.ExclusiveAccess);
                }
            }
        }

        public bool RelocateOnMove
        {
            get { return (this.FlagValue & (int)FlagBits.RelocateOnMove) != 0; }
            set
            {
                if (value == true)
                {
                    this.FlagValue |= (int)FlagBits.RelocateOnMove;
                }
                else
                {
                    this.FlagValue &= (int)~(FlagBits.RelocateOnMove);
                }
            }
        }

        public bool RedirectToFullPath
        {
            get { return (this.flagValue & (int)FlagBits.RedirectToFullPath) != 0; }
        }

        public bool RedirectionNotSpecified
        {
            get { return (this.FlagValue & (int)FlagBits.RedirectionNotSpecified) != 0; }
        }

        public int FlagValue
        {
            get { return this.flagValue; }
            set { this.flagValue = value; }
        }
    }
}
