using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WorkflowConsoleApplication1
{

    public sealed class CodeActivity1 : NativeActivity<TransitionResult>
    {
        public EventTypes EventType { get; set; }
        private BookmarkCallback cardReaderCallback;

        private BookmarkCallback CardReaderCallback
        {
            get
            {
                return this.cardReaderCallback ??
                       (this.cardReaderCallback = new BookmarkCallback(this.OnCardReaderCallback));
            }
        }

        protected override void Execute(NativeActivityContext context)
        {
            // CardReaderEvent.None is a no op

            string cardReaderEvent = this.EventType.ToString();
            context.CreateBookmark(cardReaderEvent, this.CardReaderCallback);
            

        }
        private void OnCardReaderCallback(NativeActivityContext context, Bookmark bookmark, object value)
        {
            if (value is TransitionResult)
            {
                this.Result.Set(context, value as TransitionResult);
            }
            else if (value != null)
            {
                // Resumed with something else
                throw new InvalidOperationException(
                    "You must resume the CardReader bookmark with a result of type CardReaderResult");
            }
        }
    }
}
