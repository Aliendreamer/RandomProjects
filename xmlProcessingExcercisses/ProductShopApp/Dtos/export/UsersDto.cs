namespace ProductShopApp.Dtos.export
{   
    using System;
    using System.Xml.Serialization;

    [XmlRoot("users")]
    public  class UsersDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("user")]
        public UserDtoUserInfoOnly[] Users { get; set; }

    }
}
