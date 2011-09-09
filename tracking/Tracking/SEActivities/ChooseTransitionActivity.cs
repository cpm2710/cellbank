using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace SEActivities
{
    public class ChooseTransitionActivity : NativeActivity<ChooseTransitionResult>
    {
        public ChooseTransitionCommand ChooseTransitionCommand { get; set; }
        private BookmarkCallback chooseTransitionCallback;

        private BookmarkCallback ChooseTransitionCallback
        {
            get
            {
                return this.chooseTransitionCallback ??
                       (this.chooseTransitionCallback = new BookmarkCallback(this.OnChooseTransitionCallback));
            }
        }
        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }
        protected override void Execute(NativeActivityContext context)
        {
            string cardReaderEvent = this.ChooseTransitionCommand.ToString();
            context.CreateBookmark(cardReaderEvent, this.ChooseTransitionCallback);
            
        }
        private void OnChooseTransitionCallback(NativeActivityContext context, Bookmark bookmark, object value)
        {
            if (value is ChooseTransitionResult)
            {
                this.Result.Set(context, value as ChooseTransitionResult);
            }
            else if (value != null)
            {
                throw new InvalidOperationException(
                    "You must resume the CardReader bookmark with a result of type CardReaderResult");
            }
        }
    }
}
