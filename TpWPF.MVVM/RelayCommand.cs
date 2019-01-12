using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TpWPF.MVVM
{
    /// <summary>
    ///     Classe qui implémente <see cref="ICommand"/> pour relayer les appels à des délégués.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Events

        /// <summary>
        ///     Déclenché automatiquement lorsque <see cref="CommandManager.RequerySuggested"/> est déclenché.
        ///     Permet de dire aux éléments de l'interface graphique qu'ils doivent évaluer si la méthode 
        ///     <see cref="RelayCommand.Execute(object)"/> peut être appelée en appelant 
        ///     <see cref="RelayCommand.CanExecute(object)"/>.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        #region Fields

        /// <summary>
        ///     Méthode à rappeler lorsque la méthode <see cref="RelayCommand.Execute(object)"/> est appelée.
        /// </summary>
        Action<object> _Execute;

        /// <summary>
        ///     Méthode à rappeler lorsque la méthode <see cref="RelayCommand.CanExecute(object)"/> est appelée.
        /// </summary>
        Func<object, bool> _CanExecute;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute">Méthode à rappeler pour exécuter la commande.</param>
        /// <param name="canExecute">Méthode à rappeler pour vérifier si la commande peut être exécutée.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _CanExecute = canExecute;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Détermine si la commande peut être exécutée.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécutée.</returns>
        public bool CanExecute(object parameter)
        {
            return _CanExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        ///     Exécute la commande.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        public void Execute(object parameter)
        {
            _Execute(parameter);
        }

        #endregion
    }
}
