// Commented out voor nu, misschien later toch gebruiken

using System.Collections.Generic;

namespace MarcoHelpers
{
    public enum EventName
    {
        WEEGSCHAAL_BALANCED = 0,
        ITEM_OBTAINED = 1,
        ARTICLE_CHANGE = 2,
        MENU_OPENED = 3,
        MENU_CLOSED = 4,

    }

    public delegate void EventCallback(object _value);

    public static class EventSystem
    {
        private static Dictionary<EventName, List<EventCallback>> eventRegister = new Dictionary<EventName, List<EventCallback>>();

        public static void Subscribe(EventName _evt, EventCallback _func)
        {
            if (!eventRegister.ContainsKey(_evt))
            {
                eventRegister[_evt] = new List<EventCallback>();
            }

            eventRegister[_evt].Add(_func);
        }

        public static void Unsubscribe(EventName _evt, EventCallback _func)
        {
            if (eventRegister.ContainsKey(_evt))
            {
                eventRegister[_evt].Remove(_func);
            }
        }

        public static void RaiseEvent(EventName _evt, object _value = null)
        {
            if (eventRegister.ContainsKey(_evt))
            {
                foreach (EventCallback e in eventRegister[_evt])
                {
                    e.Invoke(_value);
                }
            }
        }
    }
}

