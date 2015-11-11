using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public Text InfoTitle;
    public Image InfoImage;
    public Text InfoDesc;
    
    private RectTransform MyRect; 
    private string[] InfoStrings;

    // Use this for initialization
    void Awake () {
        AssignInfoStrings();
        MyRect = GetComponent<RectTransform>();
        MyRect.SetParent(GameObject.FindGameObjectWithTag("InfoPanelLoc")
            .transform, false);
        //transform.SetParent(GameObject.FindGameObjectWithTag("TurnManager")
        //    .transform);
        
    }
    
    // Update is called once per frame
    void Update () {
    }

    public void SetInfo(string NewTitle, Sprite NewImage, int cardType)
    {
        InfoTitle.text = NewTitle;
        InfoImage.sprite = NewImage;
        //start of first special cards is #6
        //Debug.Log("Card Type Set is: " + cardType);
        InfoDesc.text = InfoStrings[cardType-6];
    }

    /// <summary>
    /// YES I AM AWARE THIS METHOD IS SILLY THANK YOU.
    /// I just don't feel like having to code a file import
    /// or something to that effect . . .
    /// </summary>
    private void AssignInfoStrings(){
        //Debug.Log("Info Strings Assigned");
        if (Application.loadedLevelName.Contains("Pop"))
        {
            InfoStrings = new string[]{
            "The eventual leader of the Democratic party, Andrew Jackson was an accomplished general of the War of 1812, most notably known for his defense of New Orleans from the British. He was President of the United States from 1828 to 1836. ",
            "With new amendments allowing more people to vote, Andrew Jackson garnered the support of average and lower class men. This group, and the party that formed from would later be known as the democrats, citing democracy of the common man as one of its ideals.",
            "This was the political party that split from the Democratic Republican party to support John Quincy Adams and to oppose Andrew Jackson. Run by Henry Clay, this party lost two elections and it’s members moved onto the the Whig party.",
            "After the election of 1824, the Democratic-Republicans split into the Democratic Party, who supported Andrew Jackson, and the National Republican Party, who supported John Quincy Adams.",
            "With new voters coming into play, Jackson appealed to the common citizens in order to gain more support and influence. This tactic helped him with the election in 1828. Examples include rallies, parades and barbecues where they would tell people Andrew Jackson was a “common man just like you!”",
            "Vice President to Andrew Jackson from 1833-1837, and President of the United States from 1837 to 1841. He was able to help Andrew Jackson win the northern states by appealing to the northern working class men. Also a member of the unofficial “Kitchen Cabinet”",
            "This is a political tactic to show bad things about your political opponents, and to sling dirt onto their campaign. It’s considered a pretty dirty tactic, but, all's fair in love and war…. and elections. ",
            "Before the Election of 1824, constitutional amendments were being made to allow almost all white men to vote. This allowed men without property to vote in elections, and it increased the total voting percentage from 3.4% of the population in 1824, to 9.5% in 1828. Eventually, all men, regardless of race, were given the right to vote in 1870, and women were given the right in 1920. ",
            "President from 1824-1828, he was the eventual leader of the National Republican party, and ran against Andrew Jackson in both the election of 1824 and 1828. ",
            "In the election of 1824, no candidate won enough electoral votes to win the election, so House of Representatives had to decide who would be president. Henry Clay was able influence the House to elect John Quincy Adams, even though Andrew Jackson had about 40,000 more votes. The Jacksonians called this a Corrupt Bargain.",
            "Before 1824, if you were not a land-owning white man, you were not allowed to vote. However, after that year, many states removed the restriction of land ownership, meaning any white man would be allowed to vote. Running a campaign for the common man meant supporting the interests of these new voters.",
            "This was a group of friends and supporters who acted as Andrew Jackson advisors while he was in the White House. He talked to these people, instead of the actual Cabinet chosen by the Senate and President.  The “Kitchen” Cabinet is a negative term used by Jackson opponents to make his group of friends and supporters sound less important than the official “Cabinet of the President.”",
            "This was a tax on foreign goods designed to help businesses in the northern part of the United States, but it hurt the economy of the southern states. This was opposed by Andrew Jackson and the democrats. ",
            "The Spoils system was a way of rewarding those who supported a candidate in an election, by awarding those supporters jobs in the government. To the victor go the spoils.",
            "This is when elected officials would give out government jobs to their friends and family as a reward for their support during campaign season. However, these people were sometimes not very qualified for their jobs."
            };
        }
        else
        {
            InfoStrings = new string[]{
                "Being able to nullify Federal law by State.",
                "In 1832 Jackson issued a Proclamation of warning to South Carolina about not dissolving the Union.",
                "Emphasis on individual strength of the State government versus the overall power of the Federal Government.",
                "In 1833, A compromise was made for lowering tariffs without removing them completely.",
                "Andrew Jackson's forceful tariff laws and threats were seen as federal government despotism. Meaning showing power in a forceful and often cruel way.",
                "In 1833, Congress passes a law allowing the federal government to collect the tariffs by force.",
                "In January of 1830, He Gave a rousing speech to the Senate for Anti-Nullification.",
                "In 1832, this tariff raised iron & textiles law tariffs a lot and lowered others.",
                "In 1832, no longer Vice President, he can support South Carolina more effectively as it's Senator. By supporting South Carolina, maybe they can strengthen the possibility of complete Nullification of the Tariffs which Calhoun was against.",
                "In 1833, South Carolina repealed it's nullification due to Federal pressure and no support from neighboring State government. Ending the crisis.",
                "In 1833, South Carolina couldn't win support for it's tariff nullification from other states.",
                "In 1832, As a state, voted to nullify the 1828 and 1832 tariffs. Otherwise they would secede from the Union. This caused very bad tension.",
                "In 1828, tariffs began causing tension and South Carolina nullifying the tariff laws for themselves.",
                "In 1832, tension rose between him and Andrew Jackson that were caused by disagreements on if tariffs can be nullified by states. This results in his leave.",
                "iN 1828, Also known as the TARIFF OF ABOMINATIONS, this raised tariffs on iron textiles and other products. Ended up hurting southerners. This began the NULLIFICATION CRISIS."
            };
        }
    }

    public void CloseInfo()
    {
        Destroy(gameObject);
    }
}
