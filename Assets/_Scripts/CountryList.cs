using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountryList : MonoBehaviour
{
    List<string> m_DropOptions = new List<string> {"Afghanistan","Aland Islands (Finland)","Albania","Algeria","American Samoa (USA)","Andorra","Angola","Anguilla (UK)",
        "Antigua and Barbuda","Argentina","Armenia","Aruba (Netherlands)", "Australia","Austria","Azerbaijan","Bahamas","Bahrain","Bangladesh","Barbados","Belarus",
        "Belgium","Belize","Benin","Bermuda (UK)","Bhutan","Bolivia","Bosnia and Herzegovina","Botswana","Brazil","British Virgin Islands (UK)","Burkina Faso","Burma","Burundi",
        "Cambodia","Brunei","Bulgaria","Cameroon","Canada","Cape Verde","Caribbean Netherlands (Netherlands)","Cayman Islands (UK)","Central African Republic","Chad",
        "Chile","China","Christmas Island (Australia)","Cocos (Keeling) Islands (Australia)","Colombia","Comoros","Cook Islands (NZ)","Costa Rica","Croatia","Cuba","Curacao (Netherlands)",
        "Cyprus","Czech Republic","Democratic Republic of the Congo","Denmark","Djibouti","Dominica","Dominican Republic","Ecuador","Egypt","El Salvador","Equatorial Guinea","Eritrea",
        "Estonia","Ethiopia","Falkland Islands (UK)","Faroe Islands (Denmark)","Federated States of Micronesia","Fiji","Finland","France","French Guiana (France)","French Polynesia (France)",
        "Gabon","Gambia","Georgia","Germany","Ghana","Gibraltar (UK)","Greece","Greenland (Denmark)","Grenada","Guadeloupe (France)","Guam (USA)","Guatemala","Guernsey (UK)",
        "Guinea","Guinea-Bissau","Guyana","Haiti","Honduras","Hong Kong (China)","Hungary","Iceland","India","Indonesia","Iran","Iraq","Ireland","Isle of Man (UK)","Israel",
        "Italy","Ivory Coast","Jamaica","Japan","Jersey (UK)","Jordan","Kazakhstan","Kenya","Kiribati","Kosovo","Kuwait","Kyrgyzstan","Laos","Latvia","Lebanon","Lesotho","Liberia",
        "Libya","Liechtenstein","Lithuania","Luxembourg","Macau (China)","Macedonia","Madagascar","Malawi","Malaysia","Maldives","Mali","Malta","Marshall Islands","Martinique (France)",
        "Mauritania","Mauritius","Mayotte (France)","Mexico","Moldov","Monaco","Mongolia","Montenegro","Montserrat (UK)","Morocco","Mozambique","Namibia","Nauru","Nepal","Netherlands",
        "New Caledonia (France)","New Zealand","Nicaragua","Niger","Nigeria","Niue (NZ)","Norfolk Island (Australia)","North Korea","Northern Mariana Islands (USA)",
        "Norway","Oman","Pakistan","Palau","Palestine","Panama","Papua New Guinea","Paraguay","Peru","Philippines","Pitcairn Islands (UK)","Poland","Portugal","Puerto Rico",
        "Qatar","Republic of the Congo","Reunion (France)","Romania","Russia","Rwanda","Saint Barthelemy (France)","Saint Helena","Ascension and Tristan da Cunha (UK)",
        "Saint Kitts and Nevis","Saint Lucia","Saint Martin (France)","Saint Pierre and Miquelon (France)","Saint Vincent and the Grenadines","Samoa",
        "San Marino","Sao Tom and Principe","Saudi Arabia","Senegal","Serbia","Seychelles","Sierra Leone","Singapore","Sint Maarten (Netherlands)","Slovakia","Slovenia",
        "Solomon Islands","Somalia","South Africa","South Korea","South Sudan","Spain","Sri Lanka","Sudan","Suriname","Svalbard and Jan Mayen (Norway)","Switzerland",
        "Swaziland","Sweden","Syria","Taiwan","Tajikistan","Tanzania","Thailand","Timor-Leste","Togo","Tokelau (NZ)","Tonga","Trinidad and Tobago","Tunisia","Turkey",
        "Turkmenistan","Turks and Caicos Islands (UK)","Tuvalu","Uganda","Ukraine","United Arab Emirates","United Kingdom","United States","United States Virgin Islands (USA)",
        "Uruguay","Uzbekistan","Vanuatu","Vatican City","Venezuela","Vietnam","Wallis and Futuna (France)","Western Sahara","Yemen","Zambia","Zimbabwe" };


    public TMP_Dropdown m_Dropdown;

    void Start()
    {
        m_Dropdown = GetComponent<TMP_Dropdown>();

        m_Dropdown.ClearOptions();

        m_Dropdown.AddOptions(m_DropOptions);
    }

}
