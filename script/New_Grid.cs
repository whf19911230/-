using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class New_Grid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //碎片预置体，也可以在代码中创建新的UI-Image，然后给它添加一个凹凸属性的组件"grid"，就不用使用此预置体
    public GameObject grid;
    //切分个数（单边）
    public int number;
    //大图宽度,界面中显示的数据，非原图数据，用于设置界面中碎片的大小
    public float the_with;
    //主图片
    public Texture2D MainPicture;


    //生成网格
    public void CreatGrids()
    {
        if (number > 0)
        {
            float grid_size = the_with / number + the_with / (2* number);
            for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < number; j++)
                {
                    GameObject a;
                    a = Instantiate(grid, Vector3.zero, Quaternion.identity) as GameObject;
                    a.name = "grid" + (i + 1).ToString() + "-" + (j + 1).ToString();
                    a.transform.parent = GameObject.Find("Canvas").gameObject.transform;
                    RectTransform the_Rect = a.GetComponent<RectTransform>();
                    the_Rect.anchoredPosition = new Vector2(50 + grid_size * j, 300 - grid_size * i);
                }
            }

            //初始化碎片的凹凸属性,测试用,需要替换为需要的初始化规则
            {
                GameObject the_grid1;
                the_grid1 = GameObject.Find("grid3-1");
                grid Grid_mod1 = the_grid1.GetComponent<grid>();
                Grid_mod1.circle_up = 1;
                Grid_mod1.circle_right = -1;
                //Grid_mod1.circle_down = -1;
                //Grid_mod1.circle_left = -1;


                GameObject the_grid2;
                the_grid2 = GameObject.Find("grid3-2");
                grid Grid_mod2 = the_grid2.GetComponent<grid>();
                Grid_mod2.circle_up = -1;
                Grid_mod2.circle_right = 1;
                //Grid_mod2.circle_down = -1;
                Grid_mod2.circle_left = 1;

                GameObject the_grid3;
                the_grid3 = GameObject.Find("grid3-3");
                grid Grid_mod3 = the_grid3.GetComponent<grid>();
                Grid_mod3.circle_up = -1;
                //Grid_mod3.circle_right = -1;
                //Grid_mod3.circle_down = -1;
                Grid_mod3.circle_left = -1;

                GameObject the_grid4;
                the_grid4 = GameObject.Find("grid2-1");
                grid Grid_mod4 = the_grid4.GetComponent<grid>();
                Grid_mod4.circle_up = -1;
                Grid_mod4.circle_right = -1;
                Grid_mod4.circle_down = -1;
                //Grid_mod4.circle_left = -1;

                GameObject the_grid5;
                the_grid5 = GameObject.Find("grid2-2");
                grid Grid_mod5 = the_grid5.GetComponent<grid>();
                Grid_mod5.circle_up = 1;
                Grid_mod5.circle_right = 1;
                Grid_mod5.circle_down = 1;
                Grid_mod5.circle_left = 1;

                GameObject the_grid6;
                the_grid6 = GameObject.Find("grid2-3");
                grid Grid_mod6 = the_grid6.GetComponent<grid>();
                Grid_mod6.circle_up = -1;
                //Grid_mod6.circle_right = -1;
                Grid_mod6.circle_down = 1;
                Grid_mod6.circle_left = -1;

                GameObject the_grid7;
                the_grid7 = GameObject.Find("grid1-1");
                grid Grid_mod7 = the_grid7.GetComponent<grid>();
                //Grid_mod7.circle_up = -1;
                Grid_mod7.circle_right = 1;
                Grid_mod7.circle_down = 1;
                //Grid_mod7.circle_left = -1;

                GameObject the_grid8;
                the_grid8 = GameObject.Find("grid1-2");
                grid Grid_mod8 = the_grid8.GetComponent<grid>();
                //Grid_mod8.circle_up = -1;
                Grid_mod8.circle_right = 1;
                Grid_mod8.circle_down = -1;
                Grid_mod8.circle_left = -1;

                GameObject the_grid9;
                the_grid9 = GameObject.Find("grid1-3");
                grid Grid_mod9 = the_grid9.GetComponent<grid>();
                //Grid_mod9.circle_up = -1;
                //Grid_mod9.circle_right = -1;
                Grid_mod9.circle_down = 1;
                Grid_mod9.circle_left = -1;
            }
        }
    }

    //从原图对应区域逐像素点取色，复制给碎片
    public void beClip()
    {

        //循环处理每个碎片
        for (int clip_x = number; clip_x > 0; clip_x--)
        {
            for (int clip_y = 1; clip_y <= number; clip_y++)
            {
                GameObject the_grid;
                the_grid = GameObject.Find("grid" + clip_x.ToString() + "-" + clip_y.ToString());
                //切成正方形
                int w = MainPicture.width;
                int grid_size = w / number + w / (2 * number);
                Texture2D newTexture = new Texture2D(grid_size, grid_size);
                //透明一下底色
                for (int i1 = 0; i1 < grid_size; i1++)
                {
                    for (int j1 = 0; j1 < grid_size; j1++)
                    {
                        newTexture.SetPixel(i1, j1, Color.clear);
                    }
                }
                //复制正方形图片
                //获取碎片在原图中对应区域的左下角的坐标
                Vector2 yuandian_pos_big = Vector2.zero;
                yuandian_pos_big.x = (w / number) * (clip_y - 1);
                yuandian_pos_big.y = (w / number) * (number - clip_x);

                for (int i = 0; i < w / number; i++)
                {
                    for (int j = 0; j < w / number; j++)
                    {
                        Color color = MainPicture.GetPixel(i + (int)yuandian_pos_big.x, j + (int)yuandian_pos_big.y);
                        newTexture.SetPixel(i + w / (4 * number), j + w / (4 * number), color);
                    }
                }
                //判断每个边是否有凹凸，如果有就处理
                //碎片在大图中的中心坐标
                Vector2 center_pos_big = Vector2.zero;
                center_pos_big.x = (w / number) / 2 + (w / number) * (clip_y - 1);
                center_pos_big.y = (w / number) / 2 + (w / number) * (number - clip_x);
                //小正方形中心坐标
                Vector2 center_pos_small = Vector2.zero;
                center_pos_small.x = (w / number + w / (2 * number)) / 2;
                center_pos_small.y = (w / number + w / (2 * number)) / 2;

                grid grid_aotu = the_grid.GetComponent<grid>();

                //上方凹凸处理
                if (grid_aotu.circle_up == -1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x;
                    circle_pos_small.y = center_pos_small.y + (w / (2 * number)) - w / (12 * number);
                    for (int j = (int)(center_pos_small.y + (w / (2 * number))); j > center_pos_small.y + (w / (2 * number)) - (w / (4 * number)); j--)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float x0 = circle_pos_small.x - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_small.y), 2)));
                        float x1 = circle_pos_small.x + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_small.y), 2)));
                        for (int i = (int)x0; i < (int)x1; i++)
                        {
                            newTexture.SetPixel(i, j, Color.clear);
                        }
                    }
                }
                else if (grid_aotu.circle_up == 1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x;
                    circle_pos_small.y = center_pos_small.y + (w / (2 * number)) + w / (12 * number);

                    //大图中圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_big = Vector2.zero;
                    circle_pos_big.x = center_pos_big.x;
                    circle_pos_big.y = center_pos_big.y + (w / (2 * number)) + w / (12 * number);
                    //添加凸出部分要从大图像素点开始循环，便于取色
                    for (int j = (int)(center_pos_big.y + (w / (2 * number))); j < center_pos_big.y + (w / (2 * number)) + (w / (4 * number)); j++)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float x0_big = circle_pos_big.x - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_big.y), 2)));
                        float x1_big = circle_pos_big.x + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_big.y), 2)));
                        //列出此时小正方形中对应的坐标
                        int y_small = (int)(center_pos_small.y + (w / (2 * number))) + (j - (int)(center_pos_big.y + (w / (2 * number))));
                        float x0_small = circle_pos_small.x - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((y_small - circle_pos_small.y), 2)));

                        for (int i = (int)x0_big; i < (int)x1_big; i++)
                        {
                            Color color = MainPicture.GetPixel(i, j);
                            int x_small = (int)x0_small + (i - (int)x0_big);

                            newTexture.SetPixel(x_small, y_small, color);
                        }
                    }
                }

                //右边凹凸处理
                if (grid_aotu.circle_right == -1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x + (w / (2 * number)) - w / (12 * number);
                    circle_pos_small.y = center_pos_small.y;
                    for (int i = (int)(center_pos_small.x + (w / (2 * number))); i > center_pos_small.x + (w / (2 * number)) - (w / (4 * number)); i--)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float y0 = circle_pos_small.y - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_small.x), 2)));
                        float y1 = circle_pos_small.y + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_small.x), 2)));
                        for (int j = (int)y0; j < (int)y1; j++)
                        {
                            newTexture.SetPixel(i, j, Color.clear);
                        }
                    }
                }
                else if (grid_aotu.circle_right == 1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x + (w / (2 * number)) + w / (12 * number);
                    circle_pos_small.y = center_pos_small.y;

                    //大图中圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_big = Vector2.zero;
                    circle_pos_big.x = center_pos_big.x + (w / (2 * number)) + w / (12 * number);
                    circle_pos_big.y = center_pos_big.y;
                    //添加凸出部分要从大图像素点开始循环，便于取色
                    for (int i = (int)(center_pos_big.x + (w / (2 * number))); i < center_pos_big.x + (w / (2 * number)) + (w / (4 * number)); i++)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float y0_big = circle_pos_big.y - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_big.x), 2)));
                        float y1_big = circle_pos_big.y + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_big.x), 2)));
                        //列出此时小正方形中对应的坐标
                        int x_small = (int)(center_pos_small.x + (w / (2 * number))) + (i - (int)(center_pos_big.x + (w / (2 * number))));
                        float y0_small = circle_pos_small.y - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((x_small - circle_pos_small.x), 2)));

                        for (int j = (int)y0_big; j < (int)y1_big; j++)
                        {
                            Color color = MainPicture.GetPixel(i, j);
                            int y_small = (int)y0_small + (j - (int)y0_big);

                            newTexture.SetPixel(x_small, y_small, color);
                        }
                    }
                }

                //下方凹凸处理
                if (grid_aotu.circle_down == -1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x;
                    circle_pos_small.y = center_pos_small.y - (w / (2 * number)) + w / (12 * number);
                    for (int j = (int)(center_pos_small.y - (w / (2 * number))); j < center_pos_small.y - (w / (2 * number)) + (w / (4 * number)); j++)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float x0 = circle_pos_small.x - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_small.y), 2)));
                        float x1 = circle_pos_small.x + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_small.y), 2)));
                        for (int i = (int)x0; i < (int)x1; i++)
                        {
                            newTexture.SetPixel(i, j, Color.clear);
                        }
                    }
                }
                else if (grid_aotu.circle_down == 1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x;
                    circle_pos_small.y = center_pos_small.y - (w / (2 * number)) - w / (12 * number);

                    //大图中圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_big = Vector2.zero;
                    circle_pos_big.x = center_pos_big.x;
                    circle_pos_big.y = center_pos_big.y - (w / (2 * number)) - w / (12 * number);
                    //添加凸出部分要从大图像素点开始循环，便于取色
                    for (int j = (int)(center_pos_big.y - (w / (2 * number))); j > center_pos_big.y - (w / (2 * number)) - (w / (4 * number)); j--)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float x0_big = circle_pos_big.x - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_big.y), 2)));
                        float x1_big = circle_pos_big.x + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((j - circle_pos_big.y), 2)));
                        //列出此时小正方形中对应的坐标
                        int y_small = (int)(center_pos_small.y + (w / (2 * number))) + (j - (int)(center_pos_big.y + (w / (2 * number))));
                        float x0_small = circle_pos_small.x - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((y_small - circle_pos_small.y), 2)));

                        for (int i = (int)x0_big; i < (int)x1_big; i++)
                        {
                            Color color = MainPicture.GetPixel(i, j);
                            int x_small = (int)x0_small + (i - (int)x0_big);

                            newTexture.SetPixel(x_small, y_small, color);
                        }
                    }
                }

                //左边凹凸处理
                if (grid_aotu.circle_left == -1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x - (w / (2 * number)) + w / (12 * number);
                    circle_pos_small.y = center_pos_small.y;
                    for (int i = (int)(center_pos_small.x - (w / (2 * number))); i < center_pos_small.x - (w / (2 * number)) + (w / (4 * number)); i++)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float y0 = circle_pos_small.y - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_small.x), 2)));
                        float y1 = circle_pos_small.y + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_small.x), 2)));
                        for (int j = (int)y0; j < (int)y1; j++)
                        {
                            newTexture.SetPixel(i, j, Color.clear);
                        }
                    }
                }
                else if (grid_aotu.circle_left == 1)
                {
                    //小正方形凹凸圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_small = Vector2.zero;
                    circle_pos_small.x = center_pos_small.x - (w / (2 * number)) - w / (12 * number);
                    circle_pos_small.y = center_pos_small.y;

                    //大图中圆的圆心坐标，默认圆偏移边1/4个直径的长度
                    Vector2 circle_pos_big = Vector2.zero;
                    circle_pos_big.x = center_pos_big.x - (w / (2 * number)) - w / (12 * number);
                    circle_pos_big.y = center_pos_big.y;
                    //添加凸出部分要从大图像素点开始循环，便于取色
                    for (int i = (int)(center_pos_big.x - (w / (2 * number))); i > center_pos_big.x - (w / (2 * number)) - (w / (4 * number)); i--)
                    {
                        //用圆的方程求出此时对应的坐标值，圆的方程即(x - circle_pos_small.x)2+(y - circle_pos_small.y)2=(w / (6*number))2
                        float y0_big = circle_pos_big.y - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_big.x), 2)));
                        float y1_big = circle_pos_big.y + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((i - circle_pos_big.x), 2)));
                        //列出此时小正方形中对应的坐标
                        int x_small = (int)(center_pos_small.x + (w / (2 * number))) + (i - (int)(center_pos_big.x + (w / (2 * number))));
                        float y0_small = circle_pos_small.y - Mathf.Sqrt(Mathf.Abs(Mathf.Pow(w / (6 * number), 2) - Mathf.Pow((x_small - circle_pos_small.x), 2)));

                        for (int j = (int)y0_big; j < (int)y1_big; j++)
                        {
                            Color color = MainPicture.GetPixel(i, j);
                            int y_small = (int)y0_small + (j - (int)y0_big);

                            newTexture.SetPixel(x_small, y_small, color);
                        }
                    }
                }

                newTexture.Apply();

                //把纹理交给碎片
                Sprite sp = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.zero);
                Image the_image = the_grid.GetComponent<Image>();
                the_image.sprite = sp;
            }
        }
    }
}
