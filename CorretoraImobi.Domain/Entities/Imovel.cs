using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CorretoraImobi.Domain.Entities
{
    public class Imovel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id_imovel")]
        public string ID_Imovel { get; private set; }

        [BsonElement("vl_imovel")]
        public decimal VL_Imovel { get; set; }

        [BsonElement("tp_transacao")]
        public string TP_Transacao { get; set; }

        [BsonElement("tp_imovel")]
        public string TP_Imovel { get; set; }

        [BsonElement("nm_cidade")]
        public string NM_Cidade { get; set; }

        [BsonElement("estado")]
        public string Estado { get; set; }

        [BsonElement("nm_bairro")]
        public string NM_Bairro { get; set; }

        [BsonElement("ds_condominio")]
        public string DS_Condominio { get; set; }

        [BsonElement("st_estagio")]
        public string ST_Estagio { get; set; }

        [BsonElement("nm_incorporadora")]
        public string NM_Incorporadora { get; set; }

        [BsonElement("nm_construtora")]
        public string NM_Construtora { get; set; }

        [BsonElement("dt_entrega")]
        public DateTime DT_Entrega { get; set; }

        [BsonElement("diferencial")]
        public string[] Diferencial { get; set; }

        [BsonElement("lazer")]
        public string[] Lazer { get; set; }

        [BsonElement("instalacao")]
        public string[] Instalacao { get; set; }

        [BsonElement("qt_quarto")]
        public int QT_Quarto { get; set; } = 0;

        [BsonElement("qt_garagem_coberta")]
        public int QT_Garagem_Coberta { get; set; } = 0;

        [BsonElement("qt_garagem_aberta")]
        public int QT_Garagem_Aberta { get; set; } = 0;
    }
}
