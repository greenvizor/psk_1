//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace psk_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class РецептИнгредиенты
    {
        public int РецептИнгредиентId { get; set; }
        public Nullable<int> РецептId { get; set; }
        public Nullable<int> ИнгредиентId { get; set; }
        public Nullable<decimal> Количество { get; set; }
    
        public virtual Ингредиенты Ингредиенты { get; set; }
        public virtual Рецепты Рецепты { get; set; }
    }
}
