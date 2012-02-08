using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using System.Threading;
using System.Collections;

namespace Robot_simulator
{
    public class Animacija
    {
        #region RAZREDNE SPREMENLJIVKE

        public double x, y, z, a, b, c;
        private double scaleFactor = 100.0;	                                      //Osnovne enote v decimetrih, skaliranje z 100 jih pretvori v milimetre
        private double maxAnglePerSecondVelocity = 15;                            //Največji kot premika na sekundo
        private int activeToolMatrix = -1, activeFrameMatrix = -1;                //aktivni matriki, -1 za nic
        private List<Matrix4d> toolMatrices = null;	                              //koordiantni sistem orodja - TOOL
        private List<Matrix4d> toolMatricesInv = null;                            //Inverz - koordiantni sistem orodja - TOOL
        private List<Matrix4d> frameMatrices = null;		                      //koordiantni sistem uporabnika - FRAME
        private List<Vector3d> m_locations, m_orientations = null;                //Seznam lokacij in orientacij
        private int interpolationFrequencyPerSecond = 40;                         //Koliko časa računamo pozicijo robota v različnih MOV ukazih - v sekundah
        private double hitrostPrejsnjegaUkaza = -1;                               //Hitrost prejšnjega ukaza zaradi zveznega prehajanja hitrosti pri MOVL
        private Vector3d orientacijaPrejsnjegaUkaza = new Vector3d();             //Potrebujemo zaradi zveznega prehajanja orientiranosti
        private Vector3d orientacijaPrejsnjegaUkaza_originalna = new Vector3d();  //Potrebujemo zaradi zveznega prehajanja orientiranosti

        #endregion

        #region MOJE METODE

        /// <summary>
        /// Metoda izračuna razdaljo med dvema 3d točkama ( vektorjema)
        /// </summary>
        /// <param name="x1">Vrednost x prvega vektorja</param>
        /// <param name="y1">Vrednost y prvega vektorja</param>
        /// <param name="z1">Vrednost z prvega vektorja</param>
        /// <param name="x2">Vrednost x drugega vektorja</param>
        /// <param name="y2">Vrednost y drugega vektorja</param>
        /// <param name="z2">Vrednost z drugega vektorja</param>
        /// <returns></returns>
        public double Razdalja3D(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            //     _____________________________________
            //d = sqrt (x2-x1)^2 + (y2-y1)^2 + (z2-z1)^2
            //

            //rezultat
            double rezultat = 0;
            // (x2-x1)^2
            double x = Math.Pow((x2 - x1), 2);
            // (y2-y1)^2
            double y = Math.Pow((y2 - y1), 2);
            // (z2-z1)^2
            double z = Math.Pow((z2 - z1), 2);
            //Vsota vseh delov
            double vsotaVseh = x + y + z;
            //Koren vsote je razdalja
            rezultat = (double)Math.Sqrt(vsotaVseh);
            //Razdalja
            return rezultat;
        }

        #endregion

        #region RAZREDNE METODE

        /// <summary>
        /// Nastavi matriko kot matriko orodja
        /// </summary>
        /// <param name="idx">indeks matrike iz seznama matrik orodja ( tool matrices )</param>
        /// <returns>Vrne true če je nastavil, false če je prišlo do napake</returns>
        public bool setActiveToolMatrix(int idx)
        {
            if (idx == -1)
            {
                activeToolMatrix = idx;
                /*************************************************************************/
                //TO JE BLO ODKOMENTIRANO
                //this._jrobSimRef.toolMatrix = new Matrix4d();     
                //this._jrobSimRef.toolMatrix.setIdentity();       
                /*************************************************************************/
                return true;
            }

            /*************************************************************************/
            //TO JE BLO ODKOMENTIRANO
            //Matrix4d tmp = toolMatrices.get(idx);         
            //if (tmp == null)                      
            /*************************************************************************/
                return false;

            activeToolMatrix = idx;
            /*************************************************************************/
            //TO JE BLO ODKOMENTIRANO
            //this._jrobSimRef.toolMatrix = new Matrix4d(this.toolMatricesInv.get(idx));    
            /*************************************************************************/
            return true;
        }

        /// <summary>
        /// Metoda pridobi koordinate robota in jih posodobi v simulatorju - v panelu
        /// </summary>
	    public void saveCoords()
        {
            /*************************************************************************/
            //VSE JE BILO ODKOMENTIRANO

    	    //Če trenutna scena ni izbrana 	
		    //if( m_scene == null || m_scene.getRobot(0) == null)
			//    return;
    		
            //Shranimo oz pridobimo sceno robota
		    //Robot robot = m_scene.getRobot(0);
            //Vector3d qvalues = robot.getQValuesLinearized(robot.getKnuckle());	
		    //Matrix4d dh = robot.getDh().calculateDhMul(qvalues, 0);
            /*************************************************************************/
            Matrix4d dh = new Matrix4d();   //TO SE POTEM ODSTRANI

            Vector3d location = new Vector3d();
            Vector3d orientation = new Vector3d();

            CommonTools.MatrikavPRPYwithPrevious(dh, location, orientation, new Vector3d());
    		
		    x = location.X;
		    y = location.Y;
		    z = location.Z;
		    a = orientation.X;
		    b = orientation.Y;
		    c = orientation.Z;
    		
	    }
	
	    /// <summary>
	    /// Metoda shrani trenutnoe koordinate 
	    /// </summary>
	    /// <param name="location">Vektor 3d lokacij</param>
	    /// <param name="orientation">Vektor 3d orientacij</param>
        private void saveCoords(Vector3d location, Vector3d orientation)
        {
		    x = location.X;
		    y = location.Y;
		    z = location.Z;
		    a = orientation.X;
		    b = orientation.Y;
		    c = orientation.Z;
	    }	

        /// <summary>
        /// Metoda premakne robota na podano lokacijo
        /// </summary>
        /// <param name="robo_idx">Indeks robota</param>
        /// <param name="location">Lokacija, kam želimo prestavit robota - orodje, okvir, matrika morejo biti izračunani</param>
        /// <param name="orientation">Orientacija robota - orodje, okvir, matrika morejo biti izračunani</param>
        /// <returns>True če je premik uspešno izveden, drugače false</returns>
        public bool moveTo(int robo_idx, Vector3d location, Vector3d orientation)
	    {
            /*************************************************************************/
            //VSE SPODAJ JE BILO ODKOMENTIRANO

            //Dobimo trenutnega robota
		    //Robot robot = m_scene.getRobot(robo_idx);
		    //Vector3d qnames = robot.getQNamesLinearized(robot.getKnuckle());
		    //Vector3d qvalues_old = robot.getQValuesLinearized(robot.getKnuckle());
    						
		    //java.util.Hashtable qKI = robot.getIK().performIK(robot, location, orientation);
            /*************************************************************************/

		    //if(true) in { se izbriše, dodan zaradi pravilnosti kode
            if(true)
            {
            /*************************************************************************/
            //VSE SPODAJ JE BILO ODKOMENTIRANO
		    //if( qKI != null ){
    			
                //Seznam lokacij in orientacij - spremenimo na določenem indeksu v seznamu lokacijo in orientacijo
			    //m_locations.set(robo_idx, location);
			    //m_orientations.set(robo_idx, orientation );
    			
			    Vector3d settingQ = new Vector3d();
    			
			    //for( int i=0; i< qnames.size(); i++)
				//    settingQ.Add((Double)qKI.get(qnames.get(i))-(Double)qvalues_old.get(i));			
    			
			    //robot.setQValuesLinearized(robot.getKnuckle(), settingQ);
    			
			    //vrnem se sele ko se transformacije zakljucijo
			    //robot.waitForTransformationsToFinish();	
    			
			    //draw tracking test
			    //if (_jrobSimRef != null)
                //{
				//    _jrobSimRef.drawTracking();

                    bool cut = false;

                //    if (inter != null)
                //    {
                //        Object val = inter.get("OT#(11)");

                //         if (val instanceof Boolean)
                //        {
                //            cut = (Boolean)val;
                //        }
                //     }
                    
                //    if (robot.isToolActivated() || cut)
                //    {
    			//	    _jrobSimRef.drawCutting();
                //    }
                //}
            /*************************************************************************/
			    return true;
		    }		
		    else 
			    return false;
        }
    
        /// <summary>
        /// Vrne vrhnjo točko robota
        /// </summary>
        /// <param name="idx">Indeks robota</param>
        /// <param name="location">Lokacija robota</param>
        /// <param name="orientation">Orientacija robota</param>
        public void getTopOfRobot(int idx, Vector3d location, Vector3d orientation)
        {
            /*************************************************************************/
            //TO JE BLO ODKOMENTIRANO VSE
            /**************** DOBI METODE ***********************/
            //Robot robot = m_scene.getRobot(idx);                          
            //Vector qvalues = robot.getQValuesLinearized(robot.getKnuckle());
            //Matrix4d dh = robot.getDh().calculateDhMul(qvalues, 0);

            //CommonTools.MatrikavPRPYwithPrevious(dh, location, orientation, (Point3d)m_orientations.get(idx));
            /*************************************************************************/
        }

        /// <summary>
        /// Pretvorba 3d točk lokacije in orientacije v 4d matriko orodja ( tool matrics )
        /// </summary>
        /// <param name="location">3d vektor lokacije</param>
        /// <param name="orientation">3d vektor orientacije</param>
        private void transformirajTool(Vector3d location, Vector3d orientation)
	    {
            Vector3d tmp_location = new Vector3d(location);
            Vector3d tmp_orientation = new Vector3d(orientation);
		    Matrix4d tmp_matrix = CommonTools.PRPYvMatriko(tmp_location, tmp_orientation);
            //tmp_matrix.mul(frameMatricesInv.get(activeFrameMatrix));   // Najprej odstrani frame //TO NI BLO ODKOMENTIRANO

            /*************************************************************************/
            /*********TA JE BIL UPORABLJEN ****************/
            //tmp_matrix.mul(toolMatrices.get(activeToolMatrix));   // Premakni na tool point //TO JE BLO ODKOMENTIRANO
            /*************************************************************************/

            //tmp_matrix.mul(frameMatrices.get(activeFrameMatrix));   // Dodaj frame nazaj //TO NI BLO ODKOMENTIRANO
		    CommonTools.MatrikavPRPYwithPrevious(tmp_matrix, location, orientation, tmp_orientation);
        }

        /// <summary>
        /// Pretvorba 3d točk lokacije in orientacije v 4d matriko orodja ( tool matrics )
        /// </summary>
        /// <param name="location">3d vektor lokacije</param>
        /// <param name="orientation">3d vektor orientacije</param>
        private void transformirajToolInv(Vector3d location, Vector3d orientation)
        {
            Vector3d tmp_location = new Vector3d(location);
            Vector3d tmp_orientation = new Vector3d(orientation);
            Matrix4d tmp_matrix = CommonTools.PRPYvMatriko(tmp_location, tmp_orientation);
            //tmp_matrix.mul(frameMatricesInv.get(activeFrameMatrix));   // Najprej odstrani frame  //TO NI BLO ODKOMENTIRANO

            /*************************************************************************/
            /*********TA JE BIL UPORABLJEN ****************/
            //tmp_matrix.mul(toolMatricesInv[activeToolMatrix]);   // Premakni na tool point    //TO JE BLO ODKOMENTIRANO
            /*************************************************************************/

            //tmp_matrix.mul(frameMatrices.get(activeFrameMatrix));   // Dodaj frame nazaj  //TO NI BLO ODKOMENTIRANO
            CommonTools.MatrikavPRPYwithPrevious(tmp_matrix, location, orientation, tmp_orientation);
        }

        /// <summary>
        /// Če je okvir nastavljen, preračuna lokacijo in orientacijo destinacije
        /// </summary>
        /// <param name="dest_location">3d vektor lokacije destinacije</param>
        /// <param name="dest_orientation">3d vektor orientacije destinacije</param>
        /// <param name="current_orientation">3d vektor trenutne orientacije</param>
        public void getFrameOfDestination(Vector3d dest_location, Vector3d dest_orientation, Vector3d current_orientation)
        {

            Matrix4d dest = CommonTools.PRPYvMatriko(dest_location, dest_orientation);
            
            /***********SPREMENJEN VHOD V KONSTRUKTOR Z VSEMI VRSTICAMI*******************/
            Matrix4d premaknjena = new Matrix4d(dest.Row0, dest.Row1, dest.Row2, dest.Row3);

            if (activeFrameMatrix == -1 && activeToolMatrix != -1)
            {

                //samo tool			
                //premaknjena.mul(toolMatricesInv.get(activeToolMatrix));   //TO NI BLO ODKOMENTIRANO
                //premaknjena.mul(toolMatrices.get(activeToolMatrix));      //TO NI BLO ODKOMENTIRANO
                //CommonTools.MatrikavPRPYSamo(premaknjena, dest_location, dest_orientation);	//TO NI BLO ODKOMENTIRANO	    
                CommonTools.MatrikavPRPYwithPrevious(premaknjena, dest_location, dest_orientation, current_orientation);
            }
            else
            {

                //upostevam frame
                if (activeFrameMatrix != -1)
                    /*************************************************************************/
                    //premaknjena.mul(frameMatrices.get(activeFrameMatrix), dest);  //TO JE BLO ODKOMENTIRANO
                    /*************************************************************************/

                //ce je tool gor moram upostevat se to
                //if( activeToolMatrix != -1 )          //TO NI BLO ODKOMENTIRANO
                    //premaknjena.mul(toolMatricesInv.get(activeToolMatrix));   
                    //premaknjena.mul(toolMatrices.get(activeToolMatrix));    

                //CommonTools.MatrikavPRPYSamo(premaknjena, dest_location, dest_orientation);
                CommonTools.MatrikavPRPYwithPrevious(premaknjena, dest_location, dest_orientation, current_orientation);
            }
        }

        /// <summary>
        /// Nastavi trenutni okvir
        /// </summary>
        /// <param name="idx">indeks orodne matrike ( tool matrices )</param>
        /// <returns>Vrne true, če je nastavljen, false če ni</returns>
        public bool setActiveFrameMatrix(int idx)
        {
            if (idx == -1)
            {
                activeFrameMatrix = idx;
                return true;
            }

            //*****************SPREMEMBA KODE******************
            //Matrix4d tmp = frameMatrices.get(idx);
            Matrix4d tmp = frameMatrices[idx];

            if (tmp == null)
                return false;

            activeFrameMatrix = idx;

            return true;
        }

        /// <summary>
        /// Interpolacija zunanjih koordinat
        /// </summary>
        /// <param name="robot_idx">index robota</param>
        /// <param name="dest_location">Lokacija destinacije</param>
        /// <param name="dest_orientation">Orientacija destinacije</param>
        /// <param name="velocityInMMPerSecond">velocityInMMPerSecond - hitrost v mm/s</param>
        /// <param name="tool_idx">indeks orodne matrike</param>
        /// <param name="frame_idx">indeks matrike okvirja</param>
        /// <returns>True če je premik uspešen, drugače false</returns>
        public bool MOVL(int robot_idx, Vector3d dest_location, Vector3d dest_orientation,
            double velocityInMMPerSecond, int tool_idx, int frame_idx)
        {
            setActiveFrameMatrix(frame_idx);

            /*************************************************************************/
            /*********************** KI PRIDOBIŠ SCENO *********************/
            //int tool = m_scene.getRobot(robot_idx).getTool();         //TO JE BLO ODKOMENTIRANO
            /*************************************************************************/
            int tool = 0; //To je treba odstranit, ko se zgornja odkomentira

            if (tool < 0)
                tool = tool_idx;

            /*************************************************************************/
            //**********************BERE IZ JROBSIM**********************/
            //setActiveToolMatrix(tool);        //TO JE BLO ODKOMENTIRANO
            /*************************************************************************/

            //pretvorba iz mm - SPREMENJENO, DODANA 2 PARAMETRA za x y z
            dest_location.Scale(1.0 / scaleFactor, 1.0 / scaleFactor, 1.0 / scaleFactor);

            // Shrani si originalno orietacijo
            Vector3d dest_orientation_originalna = new Vector3d(dest_orientation);

            Vector3d curr_location = new Vector3d();
            Vector3d curr_orientation = new Vector3d();

            // pridobi trenutni polozaj robota
            getTopOfRobot(robot_idx, curr_location, curr_orientation);

            transformirajToolInv(curr_location, curr_orientation);
            getFrameOfDestination(dest_location, dest_orientation, (Vector3d)m_orientations[robot_idx]);

            //pretvori v mm - SKALIRANJE
            curr_location.Scale(scaleFactor, scaleFactor, scaleFactor);
            dest_location.Scale(scaleFactor, scaleFactor, scaleFactor);

            //NAMESTU TISTEGA ZGORAJ SCALE UPORABIŠ MULT ?
            //curr_location.Mult(scaleFactor);
            //dest_location.Mult(scaleFactor);

            //preracuna razdaljo premika v mm - METODA ZA RAZDALJO NI BILA NAPISANA - v javi obstaja
            //double dist = curr_location.distance(dest_location);
            double dist = Razdalja3D(curr_location.X, curr_location.Y, curr_location.Z, dest_location.X, dest_location.Y, dest_location.Z);

            //cas potovanja
            double seconds_needed = (hitrostPrejsnjegaUkaza == -1) ?
                    dist / velocityInMMPerSecond :   // Hitrost prejšnjega ukaza ni podana - nezvezno spreminjanje hitrosti (konst. hitrost)
                    dist / Math.Min(velocityInMMPerSecond, hitrostPrejsnjegaUkaza);   //Hitrost prejšnjega ukaza je podana

            //stevilo interpolacijskih tock
            double num_steps = (seconds_needed * interpolationFrequencyPerSecond);

            //smer gibanja
            Vector3d direction = new Vector3d(dest_location);
            direction.Sub(curr_location);

            //diferencialni vektor za premik v pravi smeri
            Vector3d delta_vector = new Vector3d(direction);
            delta_vector.Scale(1.0 / num_steps, 1.0 / num_steps, 1.0 / num_steps);
            //NAMESTU SCALE UPORABIMO MULT ?
            //delta_vector.Mult(1.0 / num_steps);

            Vector3d next_loc = new Vector3d(curr_location);
            //double vect_len = delta_vector.length()*num_steps;   // Zakomentiral David Zakonjšek, ker se nikjer ne uporablja

            // v = s/t
            long sleep_time = (long)(delta_vector.Length * 1000.0 / velocityInMMPerSecond);

            // Potrebujemo pri zveznem prehajanju orientacije
            Vector3d trenutnaOrientacija = (orientacijaPrejsnjegaUkaza == null) ? dest_orientation : orientacijaPrejsnjegaUkaza;
            Vector3d deltaOrientacija = new Vector3d();

            if (orientacijaPrejsnjegaUkaza != null)
            {
                deltaOrientacija = new Vector3d(dest_orientation);
                deltaOrientacija.Sub(orientacijaPrejsnjegaUkaza);
                deltaOrientacija.Scale(1.0 / num_steps, 1.0 / num_steps, 1.0 / num_steps);
                //UPORABIMO MULT ?
                //deltaOrientacija.Mult(1.0 / num_steps);
            }

            Vector3d scaled_loc = new Vector3d(curr_location);
            long start_time, tmp_sleep;

            for (int i = 0; i < num_steps; /*&& !JRobSim.control.stopJob;*/ i++)    //TO JE BILO ODKOMENTIRANO
            {
                start_time = System.DateTime.Now.Millisecond;

                double p = (double)i / num_steps;   // Razmerje prepotovane razdalje s celotno razdaljo

                // Nastavi hitrost oz. delay (v = s/t ==> t = s/v)
                if (hitrostPrejsnjegaUkaza != -1)
                {
                    double v = (p * velocityInMMPerSecond + (1 - p) * hitrostPrejsnjegaUkaza) / 2;
                    sleep_time = (long)(dist / num_steps * 1000.0 / v);
                }

                // Nastavi trenutno orientacijo, če imamo zvezno prehajanje
                if (orientacijaPrejsnjegaUkaza != null)
                    trenutnaOrientacija.Add(deltaOrientacija);
                Vector3d tmpTrenutnaOrientacija = new Vector3d(trenutnaOrientacija);

                // premik v smeri in sleep za animirano izvajanje
			    next_loc.Add(delta_vector);		
                
                //MAJHNA SPREMEMBA SCALE METODE
			    //scaled_loc.scale(1.0/scaleFactor, next_loc);
                scaled_loc.Scale(1.0 / scaleFactor, 1.0 / scaleFactor, 1.0 / scaleFactor);
                //scaled_loc.Mult(1.0 / scaleFactor); IN MULT
    			
			    //tmpMatr = CommonTools.PRPYvMatriko(scaled_loc, trenutnaOrientacija);
			    //tmpMatr.mul(toolMatrices.get(activeToolMatrix));
			    //CommonTools.MatrikavPRPYwithPrevious(tmpMatr, scaled_loc, trenutnaOrientacija, tmpTrenutnaOrientacija);
			    transformirajTool(scaled_loc, tmpTrenutnaOrientacija);
    			
                //Če ni možno izvest premik robota
			    if( moveTo(robot_idx, scaled_loc, trenutnaOrientacija) == false ){
				    //System.out.println("Error in MOVL: cannot move to specified location");
                    //Izpis napake!
                    //textBox1.Text = "Error in MOVL: cannot move to specified location";
                    //Console.WriteLine("Error in MOVL: cannot move to specified location");
				    return false;
			    }
						
			    try{								
				    //long duration = System.currentTimeMillis()-start_time;				
				    tmp_sleep = sleep_time - (System.DateTime.Now.Millisecond - start_time);
                    if (tmp_sleep > 0)
                        //Thread.Sleep(tmp_sleep);
                        Thread.Sleep(Convert.ToInt32(tmp_sleep));

			    }
			    catch( Exception e )
			    {}			
			    //if( next_loc.distance(dest_location) < 0.01){
                if(Razdalja3D(next_loc.X, next_loc.Y, next_loc.Z, dest_location.X, dest_location.Y, dest_location.Z) < 0.01){
				    break;
			    }

                /*********************************************/
                //NEVEM KAJ NAJ Z TEM
                
                /*synchronized (JRobSim.control)
                {
                    while (JRobSim.control.pauseJob)
                    {
                        try
                        {
                            JRobSim.control.wait();
                        }
                        catch (Exception e){}
                    }
                }*/
                /*********************************************/
            }

            //if (JRobSim.control.stopJob)  //KAJ NAJ S TEM POGOJEM
            if(true)    //TEGA POTEM ZBRIŠI
            {
                // pridobi trenutni polozaj robota
                dest_orientation = trenutnaOrientacija;
                dest_orientation_originalna = trenutnaOrientacija;
                saveCoords();
            }
            else
            {
                //dest_location.scale(1.0/scaleFactor);   
                dest_location.Scale(1.0 / scaleFactor, 1.0 / scaleFactor, 1.0 / scaleFactor);  
                //SPET MULT
                //dest_location.Mult(1.0 / scaleFactor);
 
                //*************TO NI BILO ODKOMENTIRANO*******************
                //tmpTrenutnaOrientacija = new Point3d(dest_orientation);
                //tmpMatr = CommonTools.PRPYvMatriko(dest_location, dest_orientation);
                //tmpMatr.mul(toolMatrices.get(activeToolMatrix));
                //CommonTools.MatrikavPRPYwithPrevious(tmpMatr, scaled_loc, trenutnaOrientacija, tmpTrenutnaOrientacija);
                //*************TO NI BILO ODKOMENTIRANO***************
             
                transformirajTool(dest_location, dest_orientation); 
                moveTo(robot_idx, dest_location, dest_orientation); 
                saveCoords();   
            }
            
            hitrostPrejsnjegaUkaza = velocityInMMPerSecond;
            orientacijaPrejsnjegaUkaza = new Vector3d(dest_orientation);
            orientacijaPrejsnjegaUkaza_originalna = dest_orientation_originalna;

            return true;
        }

        /// <summary>
        /// interpolation in inner coordinates
        /// </summary>
        /// <param name="robot_idx">index robota</param>
        /// <param name="dest_location">Lokacija destinacije</param>
        /// <param name="dest_orientation">Orientacija destinacije</param>
        /// <param name="percentOfMaxVelocity">Procent maksimalne hitrosti</param>
        /// <param name="tool_idx">indeks orodne matrike</param>
        /// <param name="frame_idx">indeks matrike okvirja</param>
        /// <returns>True če je premik uspešen, drugače false</returns>
        public bool MOVJ(int robot_idx, Vector3d dest_location, Vector3d dest_orientation,
            double percentOfMaxVelocity, int tool_idx, int frame_idx)
        {
            setActiveFrameMatrix(frame_idx);
            //setActiveToolMatrix(tool_idx);
            /*************************************************************************/
            /*********************** KI PRIDOBIŠ SCENO *********************/
            //int tool = m_scene.getRobot(robot_idx).getTool();         //TO JE BLO ODKOMENTIRANO
            /*************************************************************************/
            int tool = 0; //To je treba odstranit, ko se zgornja odkomentira

            if (tool < 0)
            {
                tool = tool_idx;
            }
            setActiveToolMatrix(tool);

            // pretvorba iz mm
            //dest_location.scale(1.0 / scaleFactor);
            dest_location.Scale(1.0 / scaleFactor, 1.0 / scaleFactor, 1.0 / scaleFactor);
            //dest_location.Mult(1.0 / scaleFactor); ALI MULT ?


            Vector3d curr_location = new Vector3d();
            Vector3d curr_orientation = new Vector3d();

            // pridobi lokacijo robota
            getTopOfRobot(robot_idx, curr_location, curr_orientation);

            transformirajToolInv(curr_location, curr_orientation);
            getFrameOfDestination(dest_location, dest_orientation, (Vector3d)m_orientations[robot_idx]);

            transformirajTool(dest_location, dest_orientation);

            if (percentOfMaxVelocity > 100.0)
                percentOfMaxVelocity = 100.0;

            //hitrost s katero bomo premikali robota
            double velocity = maxAnglePerSecondVelocity * percentOfMaxVelocity / 100.0;

            /*************************************************************************/
            //TO JE BILO ODKOMENTIRANO - PRIDOBIMO SCENO ROBOTA
            //Robot robot = m_scene.getRobot(robot_idx);
            //Vector3d qnames = robot.getQNamesLinearized(robot.getKnuckle());
            //Vector3d qvalues_old = robot.getQValuesLinearized(robot.getKnuckle());

            //Za pravilnost kode dodani objekti - pol jih zbriši!!!!
            Vector3d qnames = new Vector3d();
            Vector3d qvalues_old = new Vector3d();
            /*************************************************************************/

            //java.util.Hashtable qKI = robot.getIK().performIK(robot, dest_location, dest_orientation);
            /*************************************************************************/
            //TO JE BILO ODKOMENTIRANO
            //Hashtable qKI = robot.getIK().performIK(robot, dest_location, dest_orientation);
            /*************************************************************************/
            Hashtable qKI = new Hashtable();
            
		    if( qKI == null){
			    //System.out.println("MOVJ: cannot execute. Destination outside boundaries.");
                //Izpišemo napako, če premik ni bil možen
                //textBox1.Text = "MOVJ: cannot execute. Destination outside boundaries.";
                //Console.WriteLine("MOVJ: cannot execute. Destination outside boundaries.");
			    return false;			
		    }

            //m_locations.set(robot_idx, dest_location);
            //m_orientations.set(robot_idx, dest_orientation);
            m_locations[robot_idx] = dest_location;
            m_orientations[robot_idx] = dest_orientation;

            /*************************************************************************/
                        //TO JE BILO ODKOMENTIRANO
            //double[] settingQ = new double[qnames.size()];
            //double[] move = new double[qnames.size()];

            //Za pravilnost kode dodani objekti - pol jih zbriši!!!!
            double[] settingQ = new double[(int)qnames.Length];
            double[] move = new double[(int)qnames.Length];

            /*for (int i = 0; i < qnames.size(); i++)
            {
                settingQ[i] = (Double)qKI.get(qnames.get(i)) - (Double)qvalues_old.get(i);
                move.add(i, 0.0);
              * move[i] = 0.0;
            }
            */
            /*************************************************************************/

            int numQs = (int)qnames.Length;
            int iterations = 0;	

            // each step move at velocity angle or less
		    //while( true && !JRobSim.control.stopJob)  //TO JE TREBA ODKOMENTIRAT POGOJ
            while(true)
            {
			    iterations++;			
			    bool moved = false;			
			    for(int i = 0; i < numQs; i++)
                {
				    double tmpQ = settingQ[i];	
				    //if there is anything to move we move it at velocity at most
				    if( Math.Abs( tmpQ ) > 0.00001){
					    if( Math.Abs(tmpQ) > velocity){
						    if( tmpQ > 0 ){		//can be negative
							    //move.set(i, velocity);
                                move[i] = velocity;
							    settingQ[i] -= velocity;							
						    }
						    else{
							    //move.set(i, -velocity);
                                move[i] = -velocity;
							    settingQ[i] += velocity;
						    }
					    }
					    else{
						    //move.set(i, tmpQ);
                            move[i] = tmpQ;
						    settingQ[i] = 0.0;
					    }
					    moved = true;
				    }
				    else
					    //move.set(i, 0.0);				
                        move[i] = 0.0;
			    }
    			
			    if( !moved )
				    break;	
    			
                /*************************************************************************/
                //VSE ODKOMENTIRAT
			    //Izvedi premik robota
			    //robot.setQValuesLinearized(robot.getKnuckle(), move);						
			    long start = System.DateTime.Now.Millisecond;
			    //robot.waitForTransformationsToFinish();//to obvezno treba klicat, ker se drugace vsi gibi ne izvedejo do konca pred naslednjim klicem
			    long duration = System.DateTime.Now.Millisecond - start;
                /*************************************************************************/


                /*************************************************************************/
                //VSE ODKOMENTIRAT
                /*synchronized (JRobSim.control)
                {
                    while (JRobSim.control.pauseJob)
                    {
                        try
                        {
                            JRobSim.control.wait();
                        }
                        catch (Exception e){}
                    }
                }*/
                /*************************************************************************/

		    }

            /*************************************************************************/
            //ODKOMENTIRAT - if(true) zbrisat
            //if (JRobSim.control.stopJob)
            /*************************************************************************/
            if(true)
            {
                // pridobi trenutni polozaj robota
                getTopOfRobot(robot_idx, curr_location, curr_orientation);
                dest_orientation = curr_orientation;
            }

            hitrostPrejsnjegaUkaza = -1;
            orientacijaPrejsnjegaUkaza = new Vector3d(dest_orientation);

            return true;
        }

        /**
	     * Tri tocke zadnjega MOVC ukaza. Rabimo jih zato, da vemo ali se MOVC nadaljuje. Npr. ukaz MOVC
	     * se lahko poda 4 krat zapored, to pomeni 2 klica funkcije MOVC, od tega se pri drugem klicu funkcije
	     * ponovita dve tocki.
	     */
        private Vector3d last_MOVC_point1;
        private Vector3d last_MOVC_point2;
        private Vector3d last_MOVC_point3;

        /// <summary>
        /// Metoda izvede premik MOVC, ki se lahko izvede tudi večkrat zapored
        /// </summary>
        /// <param name="robot_idx">Indeks robota</param>
        /// <param name="movc">Vektor treh tock. V teh tockah se nahajajo koordinate same tocke in njene orientacije</param>
        /// <param name="movc_v">Vektor treh hitrosti</param>
        /// <param name="clear">spremenljivka, ki prihaja od interpreterja in je trenutno implementirana logika ne potrebuje</param>
        /// <param name="tool_idx">Indeks orodne matrike</param>
        /// <param name="frame_idx">Indeks matrike okvirja</param>
        /// <returns>true ce je premik izvedljiv, ce pride pri interpolaciji kroznice do koordinat, katerih ni mogoce prikazati, vrne false</returns>
        public bool MOVC(int robot_idx, List<Vector3d> movc, List<Vector3d> movc_v, bool clear, int tool_idx, int frame_idx)
        {
            bool risemoOdPrveDoZadnjeTocke = true;

            //iz podatkovnih struktur ekstrahiramo tocke, s katerimi bomo delali
            /*Vector3d tocka1_3d = new Vector3d(
                    ((Tocka)movc.get(0)).koordinate[0].doubleValue(),
                    ((Tocka)movc.get(0)).koordinate[1].doubleValue(),
                    ((Tocka)movc.get(0)).koordinate[2].doubleValue());*/

            Vector3d tocka1_3d = new Vector3d(movc[0].X, movc[0].Y, movc[0].Z);
            Vector3d tocka2_3d = new Vector3d(movc[1].X, movc[1].Y, movc[1].Z);
            Vector3d tocka3_3d = new Vector3d(movc[2].X, movc[2].Y, movc[2].Z);

            /*
            Vector3d orientacija1 = new Vector3d(movc[0].X, movc[0].Y, movc[0].Z);
            Vector3d orientacija2 = new Vector3d(movc[1].X, movc[1].Y, movc[1].Z);
            Vector3d orientacija3 = new Vector3d(movc[2].X, movc[2].Y, movc[2].Z);
            */

            /*************************************************************************/
            //KAJ CASTA V Tocka ( ki ma Tocka ) in kaj so koordinate 3,4,5 ?
            /*Point3d orientacija1 = new Point3d(
                ((Tocka)movc.get(0)).koordinate[3].doubleValue(),
                ((Tocka)movc.get(0)).koordinate[4].doubleValue(),
                ((Tocka)movc.get(0)).koordinate[5].doubleValue());*/

            Vector3d orientacija1 = new Vector3d();
            Vector3d orientacija2 = new Vector3d();
            Vector3d orientacija3 = new Vector3d();
            
            /*************************************************************************/

            /* Ekstrahiram hitrosti
		     * Hitrost v prvi tocki ni vazna. Ce risemo od prve do druge, uporabljamo hitrost12 (hitrost, ki je
		     * podana v drugi tocki se uporablja od prve do druge tocke), 
		     * ce risemo od druge do tretje, uporabljamo hitrost23 */

            /*************************************************************************/
            //KI PREBERE HITROSTI
            //float hitrost12 = (float)movc_v[1];
            //float hitrost23 = (float)movc_v[2];
            float hitrost12 = 0;
            float hitrost23 = 0;
            /*************************************************************************/

            //tukaj si shranimo neobdelane koordinate tocke1 in orientacije
            Vector3d tocka1_3d_originalna = new Vector3d(tocka1_3d);
            Vector3d orientacija1_originalna = new Vector3d(orientacija1);

            //nastavimo frame in tool, enako kot v drugih funkcijah
            setActiveFrameMatrix(frame_idx);
            //setActiveToolMatrix(tool_idx);

            /*************************************************************************/
            //TO JE BLO ODKOMENTIRANO _ PRIDOBIMO SCENO
            //int tool = m_scene.getRobot(robot_idx).getTool();
            /*************************************************************************/
            int tool = 0; //ZBRIŠI POTEM

            if (tool < 0)
            {
                tool = tool_idx;
            }
            setActiveToolMatrix(tool);

            //given in mm, have to transform into or units
            //tocka1_3d.scale(1.0 / scaleFactor);
            //tocka2_3d.scale(1.0 / scaleFactor);
            //tocka3_3d.scale(1.0 / scaleFactor);	

            //Skaliramo vse vrednosti v mm iz decimetrov
            tocka1_3d.Scale(1.0 / scaleFactor, 1.0 / scaleFactor, 1.0 / scaleFactor);
            tocka2_3d.Scale(1.0 / scaleFactor, 1.0 / scaleFactor, 1.0 / scaleFactor);
            tocka3_3d.Scale(1.0 / scaleFactor, 1.0 / scaleFactor, 1.0 / scaleFactor);

            //tocka1_3d.Mult(1.0 / scaleFactor);    //UPORABIMO MULT ?
            //tocka2_3d.Mult(1.0 / scaleFactor);
            //tocka3_3d.Mult(1.0 / scaleFactor);	

            //enako kot v drugih funkcijah
            //transformirajToolInv(tocka1_3d,orientacija1);
            getFrameOfDestination(tocka1_3d, orientacija1, (Vector3d)m_orientations[robot_idx]);
            //transformirajToolInv(tocka2_3d,orientacija2);
            getFrameOfDestination(tocka2_3d, orientacija2, (Vector3d)m_orientations[robot_idx]);
            //transformirajToolInv(tocka3_3d,orientacija3);
            getFrameOfDestination(tocka3_3d, orientacija3, (Vector3d)m_orientations[robot_idx]);

            // convert into milimeters. I think the base unit is decimeters ?!?
            //tocka1_3d.scale(scaleFactor);
            //tocka2_3d.scale(scaleFactor);
            //tocka3_3d.scale(scaleFactor);

            tocka1_3d.Scale(scaleFactor, scaleFactor, scaleFactor);
            tocka2_3d.Scale(scaleFactor, scaleFactor, scaleFactor);
            tocka3_3d.Scale(scaleFactor, scaleFactor, scaleFactor);

            //tocka1_3d.Mult(scaleFactor); UPORABIMO MULT ??
            //tocka2_3d.Mult(scaleFactor);
            //tocka3_3d.Mult(scaleFactor);

            //izvedemo primerjavo z zadnjimi tockami in si shranimo nove lokacije
            //Preverjamo kazalec na null, takrat prvic vstopamo v to funkcijo
            if (last_MOVC_point1 == null)
            {
                risemoOdPrveDoZadnjeTocke = true;

                last_MOVC_point1 = tocka1_3d;
                last_MOVC_point2 = tocka2_3d;
                last_MOVC_point3 = tocka3_3d;
            }
            //kontroliramo ce sta se ponovili zadnji dve iz prejšnjega niza
            else if (last_MOVC_point2 == tocka1_3d && last_MOVC_point3 == tocka2_3d)
            {
                risemoOdPrveDoZadnjeTocke = false;

                //priredimo nove tocke
                last_MOVC_point1 = tocka1_3d;
                last_MOVC_point2 = tocka2_3d;
                last_MOVC_point3 = tocka3_3d;
            }
            //MOVC se ni klical povezano v zaporedju, spet rišemo vse tri tocke
            else
            {
                risemoOdPrveDoZadnjeTocke = true;

                //priredimo nove tocke
                last_MOVC_point1 = tocka1_3d;
                last_MOVC_point2 = tocka2_3d;
                last_MOVC_point3 = tocka3_3d;
            }

            //tuakj pozenemo prvi movL da pridemo do prve tocke, ce smo ze v tej tocki, se je pac ta movL zaganjal brezveze
            if (risemoOdPrveDoZadnjeTocke)
            {
                this.MOVL(robot_idx, tocka1_3d_originalna, orientacija1_originalna, hitrost12, tool_idx, frame_idx);
            }

            /**
			 * V spodnjih vrsticah izracunam X in Y vektorja ravnine, v kateri bomo risali krivouljo 
				Ravnine, v kateri so tri tocke
			 */

            //vektor XB enotski (vektor od prve do tretje tocke)
            Vector3d XBe = new Vector3d();
            double abs = Math.Sqrt((tocka3_3d.X - tocka1_3d.X) * (tocka3_3d.X - tocka1_3d.X) +
                    (tocka3_3d.Y - tocka1_3d.Y) * (tocka3_3d.Y - tocka1_3d.Y) +
                    (tocka3_3d.Z - tocka1_3d.Z) * (tocka3_3d.Z - tocka1_3d.Z));

            XBe.X = (tocka3_3d.X - tocka1_3d.X) / abs;
            XBe.Y = (tocka3_3d.Y - tocka1_3d.Y) / abs;
            XBe.Z = (tocka3_3d.Z - tocka1_3d.Z) / abs;

            //vektor A (to je vektor od prve do druge tocke), Ae je pa enotski vektor vektorja A
            Vector3d Ae = new Vector3d();

            double absA = Math.Sqrt((tocka2_3d.X - tocka1_3d.X) * (tocka2_3d.X - tocka1_3d.X) +
            (tocka2_3d.Y - tocka1_3d.Y) * (tocka2_3d.Y - tocka1_3d.Y) +
            (tocka2_3d.Z - tocka1_3d.Z) * (tocka2_3d.Z - tocka1_3d.Z));

            Ae.X = (tocka2_3d.X - tocka1_3d.X) / absA;
            Ae.Y = (tocka2_3d.Y - tocka1_3d.Y) / absA;
            Ae.Z = (tocka2_3d.Z - tocka1_3d.Z) / absA;


            //preko vektorskega produkta dobimo ZBe. ZBe = XBe X Ae
            Vector3d ZBe = new Vector3d();
            ZBe.X = XBe.Y * Ae.Z - XBe.Z * Ae.Y;
            ZBe.Y = XBe.Z * Ae.X - XBe.X * Ae.Z;
            ZBe.Z = XBe.X * Ae.Y - XBe.Y * Ae.X;
            normalizePoint3d(ZBe);

            //sedaj pa lahko izracunamo YBe = ZBe x XBe;
            Vector3d YBe = new Vector3d();
            YBe.X = ZBe.Y * XBe.Z - ZBe.Z * XBe.Y;
            YBe.Y = ZBe.Z * XBe.X - ZBe.X * XBe.Z;
            YBe.Z = ZBe.X * XBe.Y - ZBe.Y * XBe.X;
            normalizePoint3d(YBe);

            //se pravi sedaj imamo X in Y koordinato ravnine v 3D prostoru

            //vse tri tocke pretvorimo v 2D koordinate
            //prva tocka, koordinate 0,0
            Vector2d tocka1_2d = new Vector2d(0d, 0d);

            //tretja tocka, lezi na osi X koordinate (?,0)
            Vector2d tocka3_2d = new Vector2d();

            //dolzina vektorja Xk - Xz je enaka X vrednosti tretje tocke
            tocka3_2d.X = Math.Sqrt((tocka3_3d.X - tocka1_3d.X) * (tocka3_3d.X - tocka1_3d.X) +
                    (tocka3_3d.Y - tocka1_3d.Y) * (tocka3_3d.Y - tocka1_3d.Y) +
                    (tocka3_3d.Z - tocka1_3d.Z) * (tocka3_3d.Z - tocka1_3d.Z));

            tocka3_2d.Y = 0;

            //tocka 2
            Vector2d tocka2_2d = new Vector2d();
            /**
             * na virtualni ravnini izracunamo kot fi med osjo x in vektorjem A(vektor x) krat (vektor iz izhodisca virtualnega koord. sistema do tocke 2 (vmesne tocke) -> to je v bistvu kar vektor A
             * cos fi = (a . b) /(|a|.|b|) //|a| == 1
             * S pomocjo tega kota fi pa X in Y koordinate tocke 2.
            **/
            double cosfi = (XBe.X * Ae.X + XBe.Y * Ae.Y + XBe.Z * Ae.Z);
            double fi = Math.Acos(cosfi); //ta fi pustimo kar v radianih, ker ga rabimo spodaj tudi v radianih
            tocka2_2d.X = cosfi * absA;
            tocka2_2d.Y = Math.Sin(fi) * absA;

            //enacba prve premice. Od tocke 1 do tocke 2
            double k1 = (tocka2_2d.Y - tocka1_2d.Y) / (tocka2_2d.X - tocka1_2d.X);
            double b1 = tocka1_2d.Y - k1 * tocka1_2d.X;

            //sredisce premice od tocke 1 do tocke 2
            Vector2d sredisce1 = new Vector2d();
            sredisce1.X = (tocka2_2d.X + tocka1_2d.X) / 2;
            sredisce1.Y = (tocka2_2d.Y + tocka1_2d.Y) / 2;

            //enacba simetrale premice od tocke 1 do tocke 2
            double k1_simetrala = -(1 / k1);
            double b1_simetrala = sredisce1.Y - (k1_simetrala * sredisce1.X);

            //enacba premice od tocke 2 do tocke 3
            double k2 = (tocka3_2d.Y - tocka2_2d.Y) / (tocka3_2d.X - tocka2_2d.X);
            double b2 = tocka2_2d.Y - k2 * tocka2_2d.X;

            //sredisce premice od tocke 2 do tocke 3
            Vector2d sredisce2 = new Vector2d();
            sredisce2.X = (tocka3_2d.X + tocka2_2d.X) / 2;
            sredisce2.Y = (tocka3_2d.Y + tocka2_2d.Y) / 2;

            //enacba simetrale premice od tocke 2 do tocke 3
            double k2_simetrala = -(1 / k2);
            double b2_simetrala = sredisce2.Y - (k2_simetrala * sredisce2.X);


            //presecisce simetral -> sredisce kroga
            Vector2d sredisce_kroznice = new Vector2d();
            sredisce_kroznice.X = (b2_simetrala - b1_simetrala) / (k1_simetrala - k2_simetrala);
            sredisce_kroznice.Y = k1_simetrala * sredisce_kroznice.X + b1_simetrala;

            //polmer -> razdalja med srediscem in eno izmed tock (npr. tocko1
            double polmer = Math.Sqrt((tocka1_2d.X - sredisce_kroznice.X) * (tocka1_2d.X - sredisce_kroznice.X)
                    + (tocka1_2d.Y - sredisce_kroznice.Y) * (tocka1_2d.Y - sredisce_kroznice.Y));

            //obseg kroznice
            double obsegKroznice = 2 * Math.PI * polmer;

            //kotna hitrost, glede na absolutno hitrost s katero naj se konjica premika
            double korakKota12 = (360 * hitrost12) / (obsegKroznice * interpolationFrequencyPerSecond);
            double korakKota23 = (360 * hitrost23) / (obsegKroznice * interpolationFrequencyPerSecond);

            //naredimo (predstavljamo si) nov koordinatni sistem z srediŠcem v srediŠcu kroŽnice. 
            //Imamo vektor premika. Od 0.0 do srediŠca kroŽnice
            Vector2d vektPremVRavn = new Vector2d(sredisce_kroznice.X, sredisce_kroznice.Y);

            //vse tocke premaknemo v to novo srediŠce
            Vector2d tocka1_2d_premaknjena = new Vector2d(tocka1_2d.X - vektPremVRavn.X, tocka1_2d.Y - vektPremVRavn.Y);
            Vector2d tocka2_2d_premaknjena = new Vector2d(tocka2_2d.X - vektPremVRavn.X, tocka2_2d.Y - vektPremVRavn.Y);
            Vector2d tocka3_2d_premaknjena = new Vector2d(tocka3_2d.X - vektPremVRavn.X, tocka3_2d.Y - vektPremVRavn.Y);

            //pogledam kaksni so koti alfa teh treh tock -> kot je lahko med 0 in 360°	
            double tocka1_alfa_kot = (Math.Atan2(tocka1_2d_premaknjena.Y, tocka1_2d_premaknjena.X) / Math.PI) * 180d;
            double tocka2_alfa_kot = (Math.Atan2(tocka2_2d_premaknjena.Y, tocka2_2d_premaknjena.X) / Math.PI) * 180d;
            double tocka3_alfa_kot = (Math.Atan2(tocka3_2d_premaknjena.Y, tocka3_2d_premaknjena.X) / Math.PI) * 180d;

            //vse kote spremenimo v pozitivne kote
            if (tocka1_alfa_kot < 0)
                tocka1_alfa_kot += 360d;

            if (tocka2_alfa_kot < 0)
                tocka2_alfa_kot += 360d;

            if (tocka3_alfa_kot < 0)
                tocka3_alfa_kot += 360d;

            //sedaj zavrtimo koordinatni sistem tako, da bo tocka B na osi X (kot nic stopinj). Samo Zarotiramo vse kote. Tocka B
            //ima 0 stopinj
            double premikTocke2VIzhodisce = -1 * tocka2_alfa_kot;

            double tocka1_premaknjena_alfa_kot = tocka1_alfa_kot + premikTocke2VIzhodisce;
            double tocka3_premaknjena_alfa_kot = tocka3_alfa_kot + premikTocke2VIzhodisce;

            //ce imata premaknjeni tocki 1 ali 3 negativne vrednosti, jih spravimo na pozitivne
            if (tocka1_premaknjena_alfa_kot < 0)
                tocka1_premaknjena_alfa_kot += 360d;

            if (tocka3_premaknjena_alfa_kot < 0)
                tocka3_premaknjena_alfa_kot += 360d;

            //sedaj imamo vse tri kote v vrednostih od 0 do 360°
            //druga tocka je v nuli, prva in tretja pa imata vrednosti med 0 in 1
            //true za pozitivno narascanje kota, false za negativno narascanje kota
            bool smerVrtenja;

            if (tocka3_premaknjena_alfa_kot > tocka1_premaknjena_alfa_kot)
            {
                //kot bomo zmanjsevali
                smerVrtenja = false;

                //koti si morajo slediti po številskih vrednostih od najvecjega do najmanjšega
                if (tocka3_alfa_kot > tocka2_alfa_kot)
                    tocka2_alfa_kot += 360d;
                if (tocka2_alfa_kot > tocka1_alfa_kot)
                    tocka1_alfa_kot += 360d;
            }
            //kot bomo povecevali -> vsaka tocka mora imeti vecji kot od prejsnje
            else
            {
                smerVrtenja = true;
                //koti si morajo slediti po številskih vrednostih od najmanjšega do najvecjega
                if (tocka2_alfa_kot < tocka1_alfa_kot)
                    tocka2_alfa_kot += 360d;
                if (tocka3_alfa_kot < tocka2_alfa_kot)
                    tocka3_alfa_kot += 360d;
            }

            double trenutenKot;
            double koncni_kot;
            double korakKota;
            if (risemoOdPrveDoZadnjeTocke)
            {
                trenutenKot = tocka1_alfa_kot;
                koncni_kot = tocka3_alfa_kot;
                korakKota = korakKota12;
            }
            else
            {
                trenutenKot = tocka2_alfa_kot;
                koncni_kot = tocka3_alfa_kot;
                korakKota = korakKota23;
            }

             /**interpolacija orientacije
			 * 
			 */

             /**stevilo korakov med prvo in drugo tocko
             * V spremenljivki korakKota je če notri upoštevana spremenljivka natančnost
             */

            double stKorakov12 = Math.Abs(tocka1_alfa_kot - tocka2_alfa_kot) / korakKota12;
            //stevilo korakov med drugo in tretjo tocko
            double stKorakov23 = Math.Abs(tocka2_alfa_kot - tocka3_alfa_kot) / korakKota12;

            //korak interpolacije (po vseh treh koordinatah X, Y in Z). Uporabimo strukturo Point3d
            Vector3d korakiInterpOrient12 = new Vector3d();
            Vector3d korakiInterpOrient23 = new Vector3d();

            korakiInterpOrient12.X = (orientacija2.X - orientacija1.X) / stKorakov12;
            korakiInterpOrient12.Y = (orientacija2.Y - orientacija1.Y) / stKorakov12;
            korakiInterpOrient12.Z = (orientacija2.Z - orientacija1.Z) / stKorakov12;

            korakiInterpOrient23.X = (orientacija3.X - orientacija2.X) / stKorakov12;
            korakiInterpOrient23.Y = (orientacija3.Y - orientacija2.Y) / stKorakov12;
            korakiInterpOrient23.Z = (orientacija3.Z - orientacija2.Z) / stKorakov12;

            /**
			 * Trenutno Interpolacijo inicializiramo z orientacijo v prvi tocki ali v drugi tocki, odvisno ali risemo
			 * od prve do tretje tocke ali pa od druge do tretje
			 */
            Vector3d trenutnaOrientacija = new Vector3d();
            //inicializiramo glede na tip risanja
            if (risemoOdPrveDoZadnjeTocke)
            {
                trenutnaOrientacija.X = orientacija1.X;
                trenutnaOrientacija.Y = orientacija1.Y;
                trenutnaOrientacija.Z = orientacija1.Z;
            }
            else
            {
                trenutnaOrientacija.X = orientacija2.X;
                trenutnaOrientacija.Y = orientacija2.Y;
                trenutnaOrientacija.Z = orientacija2.Z;
            }

            bool konecPremika = false;
			
            /*************************************************************************/
            //TO JE TREBA ODKOMENTIRAT
            //while(true && !JRobSim.control.stopJob)
            /*************************************************************************/
            while (true) //POTEM SE TO ODSTRANI
            {
                Vector3d novaTockaPremika = new Vector3d();

                if (smerVrtenja)
                {

                    //povecamo kot
                    double trenutni_kot_v_radianih = (trenutenKot / 360d) * 2 * Math.PI;

                    //izracunamo koordinati X in Y v ravnini v prostoru
                    double trenuten_x = polmer * Math.Cos(trenutni_kot_v_radianih) + vektPremVRavn.X;
                    double trenuten_y = polmer * Math.Sin(trenutni_kot_v_radianih) + vektPremVRavn.Y;


                    //sedaj izracunamo nove 3D koordinate
                    novaTockaPremika.X = trenuten_x * XBe.X + trenuten_y * YBe.X + tocka1_3d.X;
                    novaTockaPremika.Y = trenuten_x * XBe.Y + trenuten_y * YBe.Y + tocka1_3d.Y;
                    novaTockaPremika.Z = trenuten_x * XBe.Z + trenuten_y * YBe.Z + tocka1_3d.Z;


                    //povecamo kot
                    if ((trenutenKot + korakKota) < koncni_kot)
                        trenutenKot += korakKota;
                    //else prisli smo do kocnca -> izstopili bomo tudi iz zanke
                    else
                    {
                        konecPremika = true;
                    }
                }
                else
                {

                    //povecamo kot
                    double trenutni_kot_v_radianih = (trenutenKot / 360d) * 2 * Math.PI;

                    //izracunamo koordinati X in Y v ravnini v prostoru
                    double trenuten_x = polmer * Math.Cos(trenutni_kot_v_radianih) + vektPremVRavn.X;
                    double trenuten_y = polmer * Math.Sin(trenutni_kot_v_radianih) + vektPremVRavn.Y;

                    //sedaj izracunamo nove 3D koordinate
                    novaTockaPremika.X = (trenuten_x * XBe.X) + (trenuten_y * YBe.X) + tocka1_3d.X;
                    novaTockaPremika.Y = (trenuten_x * XBe.Y) + (trenuten_y * YBe.Y) + tocka1_3d.Y;
                    novaTockaPremika.Z = (trenuten_x * XBe.Z) + (trenuten_y * YBe.Z) + tocka1_3d.Z;


                    //zmanjsamo kot
                    if ((trenutenKot - korakKota) > koncni_kot)
                        trenutenKot -= korakKota;
                    //else prisli smo do kocnca -> izstopili bomo tudi iz zanke
                    else
                    {
                        trenutenKot = koncni_kot;
                        konecPremika = true;
                    }
                    /**Interpolacija orientacije
                     * 
                     */
                    if (risemoOdPrveDoZadnjeTocke)
                    {

                        bool smoDosegliDrugoTocko = false;
                        if (smerVrtenja)
                        {
                            if (trenutenKot > tocka2_alfa_kot)
                                smoDosegliDrugoTocko = true;
                            else
                                smoDosegliDrugoTocko = false;
                        }
                        else
                        {
                            if (trenutenKot < tocka2_alfa_kot)
                                smoDosegliDrugoTocko = true;
                            else
                                smoDosegliDrugoTocko = false;
                        }
                        //gledam ali sem med prvo in drugo tocko
                        if (!smoDosegliDrugoTocko)
                        {
                            trenutnaOrientacija.X += korakiInterpOrient12.X;
                            trenutnaOrientacija.Y += korakiInterpOrient12.Y;
                            trenutnaOrientacija.Z += korakiInterpOrient12.Z;

                        }
                        //drugace: sem med drugo in tretjo tocko
                        else
                        {
                            trenutnaOrientacija.X += korakiInterpOrient23.X;
                            trenutnaOrientacija.Y += korakiInterpOrient23.Y;
                            trenutnaOrientacija.Z += korakiInterpOrient23.Z;
                        }
                    }
                    /**drugace... risemo od druge do tretje tocke, orientacijo preprosto povecujemo za pozitivni 
				     * ali negativni delec korakiInterpOrient23
				     * 
				     */
                    else
                    {
                        trenutnaOrientacija.X += korakiInterpOrient23.X;
                        trenutnaOrientacija.Y += korakiInterpOrient23.Y;
                        trenutnaOrientacija.Z += korakiInterpOrient23.Z;
                    }


                    //na novo izracunane 3D koordinate premika skaliram nazaj
                    //novaTockaPremika.scale(1.0/scaleFactor);
                    novaTockaPremika.Scale(1.0 / scaleFactor, 1.0 / scaleFactor, 1.0 / scaleFactor);
                    //novaTockaPremika.Mult(1.0/scaleFactor); ALI MULT ?

                    Vector3d tmpTrenutnaOrientacija = new Vector3d(trenutnaOrientacija);
                    transformirajTool(novaTockaPremika, tmpTrenutnaOrientacija);
                    if (!this.moveTo(robot_idx, novaTockaPremika, tmpTrenutnaOrientacija))
                    {
                        //IZPIS NAPAKE
                        //System.out.println("Error in MOVC: cannot move to specified location");
                        //textBox1.Text = "Error in MOVC: cannot move to specified location";
                        //Console.WriteLine("Error in MOVC: cannot move to specified location");
                        saveCoords();
                        return false;
                    }
                    //zaspimo za doloceno stevilo milisekund
                    //try {
                    //	Thread.Sleep(1000 / interpolationFrequencyPerSecond);
                    /*************************************************************************/
                    //JAVI NAPAKO
                }
                //catch (InterruptedException e) {
                //catch() {
                // TODO Auto-generated catch block
                //	e.printStackTrace();
                //}
                /*************************************************************************/

                if (konecPremika)
                    break;

                /*************************************************************************/
                //TO JE TREBA ODKOMENTIRAT
                /*
                synchronized (JRobSim.control)
                {
                    while (JRobSim.control.pauseJob)
                    {
                        try
                        {
                            JRobSim.control.wait();
                        }
                        catch (Exception e){}
                    }
                }
                */
            }
            //if (JRobSim.control.stopJob)
            /*************************************************************************/
            if(true)    //TO POTEM ODSTRANI
            {
                // pridobi trenutni polozaj robota
                getTopOfRobot(robot_idx, tocka3_3d, trenutnaOrientacija);
                orientacija3 = trenutnaOrientacija;
            }
		
			saveCoords();
		
			//hitrostPrejsnjegaUkaza = hitrost23.doubleValue();
			orientacijaPrejsnjegaUkaza = new Vector3d(orientacija3);

            return true;
        }

        /// <summary>
        /// Metoda normalizira točko
        /// </summary>
        /// <param name="tocka">3d točka - vektor</param>
        public static void normalizePoint3d(Vector3d tocka)
        {
            //normaliziram, uporabim normalize funkcijo od Vector3d
            Vector3d vector = new Vector3d(tocka);
            vector.Normalize();

            //updejtam Point3d
            tocka.X = vector.X;
            tocka.Y = vector.Y;
            tocka.Z = vector.Z;
        }

        #endregion


    }
}
