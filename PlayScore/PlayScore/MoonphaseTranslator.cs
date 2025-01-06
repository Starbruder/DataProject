namespace WpfTestApp
{
    public class MoonphaseTranslator
    {
        private readonly Dictionary<string, string> _moonPhaseTranslations = new Dictionary<string, string>
        {
            { "NEW_MOON", "Neumond" },
            { "WAXING_CRESCENT", "Zunehmender Halbmond" },
            { "FIRST_QUARTER", "Erstes Viertel" },
            { "WAXING_GIBBOUS", "Zunehmender Dreiviertelmond" },
            { "FULL_MOON", "Vollmond" },
            { "WANING_GIBBOUS", "Abnehmender Dreiviertelmond" },
            { "LAST_QUARTER", "Letztes Viertel" },
            { "WANING_CRESCENT", "Abnehmender Halbmond" }
        };

        public string Translate(string moonPhase)
        {
            if (_moonPhaseTranslations.TryGetValue(moonPhase, out string translation))
            {
                return translation;
            }
            else
            {
                // Return the original name if no translation is found
                return moonPhase;
            }
        }
    }
}
