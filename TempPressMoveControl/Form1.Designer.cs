namespace TempPressMoveControl
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tx_CurPress = new System.Windows.Forms.TextBox();
            this.tx_CurPos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tx_CircleTimes = new System.Windows.Forms.TextBox();
            this.tx_PointMove = new System.Windows.Forms.TextBox();
            this.tx_Rate = new System.Windows.Forms.TextBox();
            this.tx_Distance = new System.Windows.Forms.TextBox();
            this.tx_TarPress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tx_shidu = new System.Windows.Forms.TextBox();
            this.tx_Temp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tab_OperatePages = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btn_StopGoBankTest = new System.Windows.Forms.Button();
            this.btn_StartGoBankTest = new System.Windows.Forms.Button();
            this.btn_returnZero = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_MoveToPress = new System.Windows.Forms.Button();
            this.btn_XMinus = new System.Windows.Forms.Button();
            this.btn_XPlus = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.btn_StopSetshidu = new System.Windows.Forms.Button();
            this.btn_StopSetTemp = new System.Windows.Forms.Button();
            this.btn_Setshidu = new System.Windows.Forms.Button();
            this.btn_SetTemp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tx_PassWord = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tab_OperatePages.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tx_CurPress);
            this.groupBox1.Controls.Add(this.tx_CurPos);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(667, 562);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "实时数据";
            // 
            // tx_CurPress
            // 
            this.tx_CurPress.Location = new System.Drawing.Point(331, 26);
            this.tx_CurPress.Name = "tx_CurPress";
            this.tx_CurPress.Size = new System.Drawing.Size(100, 21);
            this.tx_CurPress.TabIndex = 1;
            // 
            // tx_CurPos
            // 
            this.tx_CurPos.Location = new System.Drawing.Point(123, 26);
            this.tx_CurPos.Name = "tx_CurPos";
            this.tx_CurPos.Size = new System.Drawing.Size(100, 21);
            this.tx_CurPos.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(246, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "当前压力:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前位置(mm):";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(685, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(256, 110);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(248, 84);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "运动控制卡";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(181, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "打开连接";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(248, 84);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "温湿度控制器";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(181, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 50);
            this.button2.TabIndex = 1;
            this.button2.Text = "打开连接";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(248, 84);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "压力测试仪";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(184, 9);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(61, 50);
            this.button3.TabIndex = 2;
            this.button3.Text = "打开连接";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(685, 128);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(252, 154);
            this.tabControl2.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tx_CircleTimes);
            this.tabPage4.Controls.Add(this.tx_PointMove);
            this.tabPage4.Controls.Add(this.tx_Rate);
            this.tabPage4.Controls.Add(this.tx_Distance);
            this.tabPage4.Controls.Add(this.tx_TarPress);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(244, 128);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "运动参数";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tx_CircleTimes
            // 
            this.tx_CircleTimes.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_CircleTimes.Location = new System.Drawing.Point(89, 93);
            this.tx_CircleTimes.Name = "tx_CircleTimes";
            this.tx_CircleTimes.Size = new System.Drawing.Size(133, 18);
            this.tx_CircleTimes.TabIndex = 1;
            // 
            // tx_PointMove
            // 
            this.tx_PointMove.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_PointMove.Location = new System.Drawing.Point(89, 71);
            this.tx_PointMove.Name = "tx_PointMove";
            this.tx_PointMove.Size = new System.Drawing.Size(133, 18);
            this.tx_PointMove.TabIndex = 1;
            // 
            // tx_Rate
            // 
            this.tx_Rate.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_Rate.Location = new System.Drawing.Point(89, 29);
            this.tx_Rate.Name = "tx_Rate";
            this.tx_Rate.Size = new System.Drawing.Size(133, 18);
            this.tx_Rate.TabIndex = 1;
            // 
            // tx_Distance
            // 
            this.tx_Distance.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_Distance.Location = new System.Drawing.Point(89, 50);
            this.tx_Distance.Name = "tx_Distance";
            this.tx_Distance.Size = new System.Drawing.Size(133, 18);
            this.tx_Distance.TabIndex = 1;
            // 
            // tx_TarPress
            // 
            this.tx_TarPress.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_TarPress.Location = new System.Drawing.Point(89, 6);
            this.tx_TarPress.Name = "tx_TarPress";
            this.tx_TarPress.Size = new System.Drawing.Size(133, 18);
            this.tx_TarPress.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "循环次数:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "点动(mm):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "距离(mm):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "频率(Hz):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "目标压力(N):";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.tx_shidu);
            this.tabPage5.Controls.Add(this.tx_Temp);
            this.tabPage5.Controls.Add(this.label8);
            this.tabPage5.Controls.Add(this.label9);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(244, 128);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "温湿度参数";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tx_shidu
            // 
            this.tx_shidu.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_shidu.Location = new System.Drawing.Point(89, 32);
            this.tx_shidu.Name = "tx_shidu";
            this.tx_shidu.Size = new System.Drawing.Size(133, 18);
            this.tx_shidu.TabIndex = 4;
            // 
            // tx_Temp
            // 
            this.tx_Temp.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tx_Temp.Location = new System.Drawing.Point(89, 9);
            this.tx_Temp.Name = "tx_Temp";
            this.tx_Temp.Size = new System.Drawing.Size(133, 18);
            this.tx_Temp.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "湿度:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "温度(℃):";
            // 
            // tab_OperatePages
            // 
            this.tab_OperatePages.Controls.Add(this.tabPage6);
            this.tab_OperatePages.Controls.Add(this.tabPage7);
            this.tab_OperatePages.Location = new System.Drawing.Point(685, 343);
            this.tab_OperatePages.Name = "tab_OperatePages";
            this.tab_OperatePages.SelectedIndex = 0;
            this.tab_OperatePages.Size = new System.Drawing.Size(256, 231);
            this.tab_OperatePages.TabIndex = 3;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btn_StopGoBankTest);
            this.tabPage6.Controls.Add(this.btn_StartGoBankTest);
            this.tabPage6.Controls.Add(this.btn_returnZero);
            this.tabPage6.Controls.Add(this.btn_Stop);
            this.tabPage6.Controls.Add(this.btn_MoveToPress);
            this.tabPage6.Controls.Add(this.btn_XMinus);
            this.tabPage6.Controls.Add(this.btn_XPlus);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(248, 205);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "运动控制";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // btn_StopGoBankTest
            // 
            this.btn_StopGoBankTest.Location = new System.Drawing.Point(19, 162);
            this.btn_StopGoBankTest.Name = "btn_StopGoBankTest";
            this.btn_StopGoBankTest.Size = new System.Drawing.Size(203, 23);
            this.btn_StopGoBankTest.TabIndex = 1;
            this.btn_StopGoBankTest.Text = "停止往复测试";
            this.btn_StopGoBankTest.UseVisualStyleBackColor = true;
            // 
            // btn_StartGoBankTest
            // 
            this.btn_StartGoBankTest.Location = new System.Drawing.Point(19, 133);
            this.btn_StartGoBankTest.Name = "btn_StartGoBankTest";
            this.btn_StartGoBankTest.Size = new System.Drawing.Size(203, 23);
            this.btn_StartGoBankTest.TabIndex = 1;
            this.btn_StartGoBankTest.Text = "启动往复测试";
            this.btn_StartGoBankTest.UseVisualStyleBackColor = true;
            // 
            // btn_returnZero
            // 
            this.btn_returnZero.Location = new System.Drawing.Point(19, 104);
            this.btn_returnZero.Name = "btn_returnZero";
            this.btn_returnZero.Size = new System.Drawing.Size(203, 23);
            this.btn_returnZero.TabIndex = 1;
            this.btn_returnZero.Text = "回零";
            this.btn_returnZero.UseVisualStyleBackColor = true;
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(19, 75);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(203, 23);
            this.btn_Stop.TabIndex = 1;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            // 
            // btn_MoveToPress
            // 
            this.btn_MoveToPress.Location = new System.Drawing.Point(19, 46);
            this.btn_MoveToPress.Name = "btn_MoveToPress";
            this.btn_MoveToPress.Size = new System.Drawing.Size(203, 23);
            this.btn_MoveToPress.TabIndex = 1;
            this.btn_MoveToPress.Text = "移动至压力点";
            this.btn_MoveToPress.UseVisualStyleBackColor = true;
            // 
            // btn_XMinus
            // 
            this.btn_XMinus.Location = new System.Drawing.Point(147, 17);
            this.btn_XMinus.Name = "btn_XMinus";
            this.btn_XMinus.Size = new System.Drawing.Size(75, 23);
            this.btn_XMinus.TabIndex = 0;
            this.btn_XMinus.Text = "X-";
            this.btn_XMinus.UseVisualStyleBackColor = true;
            // 
            // btn_XPlus
            // 
            this.btn_XPlus.Location = new System.Drawing.Point(19, 17);
            this.btn_XPlus.Name = "btn_XPlus";
            this.btn_XPlus.Size = new System.Drawing.Size(75, 23);
            this.btn_XPlus.TabIndex = 0;
            this.btn_XPlus.Text = "X+";
            this.btn_XPlus.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.btn_StopSetshidu);
            this.tabPage7.Controls.Add(this.btn_StopSetTemp);
            this.tabPage7.Controls.Add(this.btn_Setshidu);
            this.tabPage7.Controls.Add(this.btn_SetTemp);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(248, 205);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "温湿度控制";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // btn_StopSetshidu
            // 
            this.btn_StopSetshidu.Location = new System.Drawing.Point(18, 102);
            this.btn_StopSetshidu.Name = "btn_StopSetshidu";
            this.btn_StopSetshidu.Size = new System.Drawing.Size(204, 23);
            this.btn_StopSetshidu.TabIndex = 0;
            this.btn_StopSetshidu.Text = "停止湿度设置";
            this.btn_StopSetshidu.UseVisualStyleBackColor = true;
            // 
            // btn_StopSetTemp
            // 
            this.btn_StopSetTemp.Location = new System.Drawing.Point(18, 44);
            this.btn_StopSetTemp.Name = "btn_StopSetTemp";
            this.btn_StopSetTemp.Size = new System.Drawing.Size(204, 23);
            this.btn_StopSetTemp.TabIndex = 0;
            this.btn_StopSetTemp.Text = "停止温度设置";
            this.btn_StopSetTemp.UseVisualStyleBackColor = true;
            // 
            // btn_Setshidu
            // 
            this.btn_Setshidu.Location = new System.Drawing.Point(18, 73);
            this.btn_Setshidu.Name = "btn_Setshidu";
            this.btn_Setshidu.Size = new System.Drawing.Size(204, 23);
            this.btn_Setshidu.TabIndex = 0;
            this.btn_Setshidu.Text = "启动湿度设置";
            this.btn_Setshidu.UseVisualStyleBackColor = true;
            // 
            // btn_SetTemp
            // 
            this.btn_SetTemp.Location = new System.Drawing.Point(18, 15);
            this.btn_SetTemp.Name = "btn_SetTemp";
            this.btn_SetTemp.Size = new System.Drawing.Size(204, 23);
            this.btn_SetTemp.TabIndex = 0;
            this.btn_SetTemp.Text = "启动温度设置";
            this.btn_SetTemp.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tx_PassWord);
            this.groupBox2.Location = new System.Drawing.Point(685, 288);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 49);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作密码";
            // 
            // tx_PassWord
            // 
            this.tx_PassWord.Location = new System.Drawing.Point(12, 20);
            this.tx_PassWord.Name = "tx_PassWord";
            this.tx_PassWord.Size = new System.Drawing.Size(214, 21);
            this.tx_PassWord.TabIndex = 0;
            this.tx_PassWord.TextChanged += new System.EventHandler(this.tx_PassWord_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 586);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tab_OperatePages);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "直线式循环往复测试仪";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tab_OperatePages.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tx_CurPress;
        private System.Windows.Forms.TextBox tx_CurPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox tx_CircleTimes;
        private System.Windows.Forms.TextBox tx_PointMove;
        private System.Windows.Forms.TextBox tx_Rate;
        private System.Windows.Forms.TextBox tx_Distance;
        private System.Windows.Forms.TextBox tx_TarPress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tx_shidu;
        private System.Windows.Forms.TextBox tx_Temp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabControl tab_OperatePages;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button btn_returnZero;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_MoveToPress;
        private System.Windows.Forms.Button btn_XMinus;
        private System.Windows.Forms.Button btn_XPlus;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Button btn_StopGoBankTest;
        private System.Windows.Forms.Button btn_StartGoBankTest;
        private System.Windows.Forms.Button btn_StopSetshidu;
        private System.Windows.Forms.Button btn_StopSetTemp;
        private System.Windows.Forms.Button btn_Setshidu;
        private System.Windows.Forms.Button btn_SetTemp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tx_PassWord;
    }
}

