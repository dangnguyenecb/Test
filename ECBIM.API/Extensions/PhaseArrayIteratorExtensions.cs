using Autodesk.Revit.DB;

namespace ECBIM.API.Extensions
{
    public static class PhaseArrayIteratorExtensions
    {
        /// <summary>
        /// Positionner un PhaseArrayIterator à partir d'un nom de phase
        /// </summary>
        /// <param name="phaseArrayIterator">PhaseArrayIterator à configurer</param>
        /// <param name="phaseName">Nom de la phase voulue</param>
        /// <returns>PhaseArrayIterator positionné à la phase donnée</returns>
        public static PhaseArrayIterator MoveToPhase(this PhaseArrayIterator phaseArrayIterator, string phaseName)
        {
            phaseArrayIterator.Reset();
            bool keepRunning = true;

            while (keepRunning)
            {
                phaseArrayIterator.MoveNext();
                Phase ph = phaseArrayIterator.Current as Phase;

                if (ph.Name.Equals(phaseName))
                {
                    keepRunning = false;
                }
            }

            return phaseArrayIterator;
        }

        /// <summary>
        /// Vérifier s'il reste un nombre de phases disponibles dans un itérateur à partir de sa position actuelle
        /// </summary>
        /// <param name="phaseArrayIterator">PhaseArrayIterator à vérifier</param>
        /// <param name="n">Nombre de phases</param>
        /// <returns>True s'il reste assez de phases, False s'il n'en reste pas assez</returns>
        public static bool HaveEnoughPhases(this PhaseArrayIterator phaseArrayIterator, int n)
        {
            Phase currentPhase = phaseArrayIterator.Current as Phase;

            for (int i = 1; i < n; i++)
            {
                if (phaseArrayIterator.MoveNext())
                {
                    continue;
                }
                else
                {
                    phaseArrayIterator.MoveToPhase(currentPhase.Name);
                    return false;
                }
            }

            phaseArrayIterator.MoveToPhase(currentPhase.Name);

            return true;
        }
    }
}
