
namespace Droid.Litterature
{
    public enum LANGAGE
    {
        FR,
        NONE
    }
    public enum PONCTUATION
    {
        UNKNOWN,
        EXCLAMATION,
        QUESTION,
        AFFIRMATION
    }
    public enum GENRE
    {
        UNKNOWN,
        MASCULIN,
        FEMININ,
        NEUTRE
    }
    public enum NOMBRE
    {
        UNKNOWN,
        SINGULIER,
        PLURIEL,
        NEUTRE
    }
    public enum ROLE
    {
        UNKNOWN,
        VERBE,
        ADJECTIF,
        ADVERBE,
        PRONOM,
        NOMCOMMUN,
        NOMPROPRE,
        CONJONCTION,
        DETERMINANT,
        PREPOSITION,
        CHIFFRE,
        MAIL
    }
    public enum ADJECTIVE
    {
        APPARENCE,
        PERSONNALITE,
        SENTIMENT,
        FORME,
        ETENDUE,
        TEMPS,
        QUANTITE,
        SON,
        GOUT,
        TOUCHER,
        COULEUR
    }
    public enum ADVERB
    {
        UNKNOWN,
        LIEU,
        TEMPS,
        MANIERE,
        QUANTITE,
        AFFIRMATION,
        NEGATION,
        RELATION,
        LIAISON,
        MODAL,
        QUESTION
    }
    public enum CONJONCTION
    {
        UNKNOWN,
        COORDINATION,
        SUBORDINATION,
        LOCUTION
    }
    public enum NIVEAU_LANG
    {
        SOUTENU,
        COURANT,
        FAMILLIER,
        ARGOT
    }
    public enum STRUCTURE_COMPLEXE
    {
        GROUPE_NOMINAL,
        GROUPE_VERBAL,
        COD,
        COI,
        SUPERLATIF,
        SUJET,
        ATTRIBUT_DU_SUJET,
        UNKNOW
    }
}