using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibRazor
{


    public class MensagemConfirmacaoCs : ComponentBase, IDisposable
    {

        [Parameter] public int Counter { get; set; }
        [Parameter] public bool ExibirConfirmacao { get; set; } = false;
        [Parameter] public string Titulo { get; set; } = "Confirma Exclusão ?";
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback onConfirma { get; set; }
        [Parameter] public EventCallback onCancela { get; set; }

        public void Exibir() => ExibirConfirmacao = true;
        public void Ocultar() => ExibirConfirmacao = false;

        #region " Clico de vida dos componentes "
        /*class base*/
        //public class MensagemConfirmacaoCs : ComponentBase, IDisposable

        bool disposed = false;

        protected override void OnInitialized()
        {
            // Atualiza a renderização 
            /*
                StateHasChanged(); 
             */


            base.OnInitialized();
        }



        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }

        protected override void OnParametersSet()
        {
            // base.OnParametersSet();
            counterOutput = Counter.ToString() + " and counting...";
        }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }

        protected override bool ShouldRender()
        {
            /*se retornar true vai ser atualizado*/
            return base.ShouldRender();
        }



        public override async Task SetParametersAsync(ParameterView parameters)
        {

            await base.SetParametersAsync(parameters);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {

                // Free any other managed objects here.
                //
            }

            disposed = true;
        }

        #endregion



        public string counterOutput { get; set; }


       
    }
}
