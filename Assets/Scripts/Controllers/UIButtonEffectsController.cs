//Тупая реализация.
//Нажали одну кнопку неважно где, прогнали всю цепочку подписчиков на вообще все события нажатия всех кнопок.
//Поможет ли здесь System.ComponentModel.EventHandlerList?
//Или поделить на более мелкие меню и наборы функций (и енумов тоже несколько), чтобы на каждый было по отдельному ивенту?

using System;
using System.ComponentModel;

public class UIButtonEffectsController
{
    public event Action<MenuButtons> OnButtonPressed;
    public event Action<MenuButtons> OnButtonPressedEarly;
    public event Action<MenuButtons> OnButtonPressedLate;

    public void ButtonEffectRequested(MenuButtons button)
    {
        ///Предварительная цепочка события "нажата кнопка", которая запускается перед основной.
        ///Если важно, чтобы по нажатию кнопки СНАЧАЛА выполнилось некое действие А, а ПОТОМ уже отработала остальная цепочка,
        ///то действие А нужно подписать на OnButtonPressedEarly.
        OnButtonPressedEarly?.Invoke(button);

        ///Основная цепочка события "нажата кнопка"
        OnButtonPressed?.Invoke(button);

        ///Дополнительная цепочка события "нажата кнопка", которая запускается после основной.
        ///Если нужно, чтобы по нажатию кнопки СНАЧАЛА отработала основная цепочка, а ПОТОМ выполнилось действие А,
        ///то действие А нужно подписать на OnButtonPressedLate.
        OnButtonPressedLate?.Invoke(button);
    }
    
    
    
    //Смотрим EventHandlerList:

    //private EventHandlerList _events = new EventHandlerList();

    //public event EventHandler OnCreditsButtonPressed
    //{
    //    add
    //    {
    //        _events.AddHandler(MenuButtons.Credits, value);
    //    }

    //    remove
    //    {
    //        _events.AddHandler(MenuButtons.Credits, value);
    //    }
    //}

    //private void LaunchCreditsButtonPressedEvent()
    //{
    //    var evnt = (EventHandler)_events[MenuButtons.Credits];
    //    evnt?.Invoke(this, EventArgs.Empty);
    //}

    //и т.д. под каждую кнопку?

}
