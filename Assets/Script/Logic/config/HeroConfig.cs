/// <summary>
/// 英雄配置表
/// </summary>
public class HeroConfig : BaseConfig
{
    public int id;
    public int tid;
    public string name;
    public int rarity;//品质 白绿蓝紫橙
    public string portrait;
    public int camp;//阵营
    public string campIcon;//阵营图标
    public int clazz;//职业
    public int tags;//标签
    public string water;//水属性
    public string fire;//火属性
    public string air;//风属性
    public string earth;//土属性
    public int activeSpellID;//主动技能id
    public int passiveSpellID;//被动技能id
    public int heroobtaindialogue;//获得英雄时候的故事id
    //public int normalSpellID;//普通攻击id
    public int entanglement;//羁绊id
    public double attackBase;//物理攻击基础值
    public double attackGrowth;//物理攻击增加量
    public double spellBase;//魔法攻击基础值
    public double spellGrowth;//魔法攻击增加量
    public double armorBase;//物理防御基础值
    public double armorGrowth;//物理防御增加量
    public double resistanceBase;//魔法防御基础值
    public double resistanceGrowth;//魔法防御增加量
    public double speedBase;//速度基础值
    public double speedGrowth;//速度增加量
    public string avatar;//头像
    public string cutin;//半身
    public string description;//介绍
    public string battleModel; // 战斗场景使用的模型
    public string sceneModel;
    public string sceneStoneModel; // 23.7.25 爬塔等地方用的石像
    public string scenewalk;
    public string scenerun;
    public string sceneinjuryretreat;
    public float OccupiedActPlayTime;
    public string model;//UI 场景展示的某些资源
    public string scene;//UI 模型场景
    public double[] portraitHead;//头像偏移
    public int talentID;//天赋ID
    public double towersize;
    public string HeroScene;
}