using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CorneTest.Models;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks.Dataflow;
using ForAgents.Models;

namespace CorneTest
{
    public class Databases
    {
        public string ConnStr
        {
            get
            {
                var server = ConfigurationManager.AppSettings["DBServer"].ToString();
                var userName = ConfigurationManager.AppSettings["DBUserName"].ToString();
                var password = ConfigurationManager.AppSettings["DBPassword"].ToString();
                var database = ConfigurationManager.AppSettings["DBName"].ToString();
                if (String.IsNullOrEmpty(userName))
                {
                    return $"Data Source={server};Initial Catalog={database};Integrated Security=SSPI";
                }
                else
                {
                    return $"Data Source={server};Initial Catalog={database};Persist Security Info=true; User ID={userName};Password={password}";
                }
            }
        }

        public int InsertListing(ListingInitiate listing)
        {
            int i = 0;

            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();
                string sqlQuery = @"EXEC fa_Insert_Listing @Option, @AgentSequenceNumber, @DateListed, @StandNumber, @StreetAddress, @AddressLine2, @City, 
                                        @StateProvinceRegion, @ZipPostalAreaCode, @ReasonForSelling, @Category";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@Option", "INSERT_LISTING");
                    cmd.Parameters.AddWithValue("@AgentSequenceNumber", listing.AgentNumber ?? "");
                    cmd.Parameters.AddWithValue("@DateListed", listing.DateListed ?? "");
                    cmd.Parameters.AddWithValue("@StandNumber", listing.StandNumber ?? "");
                    cmd.Parameters.AddWithValue("@StreetAddress", listing.StreetAddress ?? "");
                    cmd.Parameters.AddWithValue("@AddressLine2", listing.AddressLine2 ?? "");
                    cmd.Parameters.AddWithValue("@city", listing.City ?? "");
                    cmd.Parameters.AddWithValue("@StateProvinceRegion", listing.StateProvinceRegion ?? "");
                    cmd.Parameters.AddWithValue("@ZipPostalAreaCode", listing.ZipPostalAreaCode ?? "");
                    cmd.Parameters.AddWithValue("@ReasonForSelling", listing.ReasonForSelling ?? "");
                    cmd.Parameters.AddWithValue("@Category", listing.Category ?? "");

                    i = cmd.ExecuteNonQuery();
                }
                cn.Close();
                return i;
            }
        }

        public int GetLastListingID()
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();
                string sqlQuery = @"EXEC fa_Insert_Listing @Option, @AgentSequenceNumber, @DateListed, @StandNumber, @StreetAddress, @AddressLine2, @City, 
                                        @StateProvinceRegion, @ZipPostalAreaCode, @ReasonForSelling, @Category";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@Option", "GET_LAST_ID");
                    cmd.Parameters.AddWithValue("@AgentSequenceNumber", DBNull.Value);
                    cmd.Parameters.AddWithValue("@DateListed", DBNull.Value);
                    cmd.Parameters.AddWithValue("@StandNumber", DBNull.Value);
                    cmd.Parameters.AddWithValue("@StreetAddress", DBNull.Value);
                    cmd.Parameters.AddWithValue("@AddressLine2", DBNull.Value);
                    cmd.Parameters.AddWithValue("@city", DBNull.Value);
                    cmd.Parameters.AddWithValue("@StateProvinceRegion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ZipPostalAreaCode", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ReasonForSelling", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Category", DBNull.Value);

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            return Convert.ToInt32(rd["Id"]);
                        }
                    }
                }
                cn.Close();

            }
            return 0;
        }

        public void InsertListingType(TypeDto type)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Listing_Type_Source @ListingId, @ListingType, @ListingSource, @ListingSourceDetails";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingId", type.ListingId);
                    cmd.Parameters.AddWithValue("@ListingType", type.ListingType ?? "");
                    cmd.Parameters.AddWithValue("@ListingSource", type.Source ?? "");
                    cmd.Parameters.AddWithValue("@ListingSourceDetails", type.SourceDetails ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertSellerDetails(Seller seller)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Seller_Details @ListingID, @SellerNumber1, @LegalEntitySeller1, @RegistrationNumSeller1, @SurnameSeller1, @FirstNameSeller1,
                                    @MobileSeller1, @PhoneSeller1, @EmailSeller1, @IDNumberSeller1, @PostAddressSeller1,
                                    @SellerNumber2, @LegalEntitySeller2, @RegistrationNumSeller2, @SurnameSeller2, @FirstNameSeller2,
                                    @MobileSeller2, @PhoneSeller2, @EmailSeller2, @IDNumberSeller2, @PostAddressSeller2 ";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingId", seller.ListingId);
                    cmd.Parameters.AddWithValue("@SellerNumber1", seller.SellerNum1);
                    cmd.Parameters.AddWithValue("@LegalEntitySeller1", seller.LegalEntitySeller1 ?? "");
                    cmd.Parameters.AddWithValue("@RegistrationNumSeller1", seller.RegistrationNumberSeller1 ?? "");
                    cmd.Parameters.AddWithValue("@SurnameSeller1", seller.SurnameSeller1 ?? "");
                    cmd.Parameters.AddWithValue("@FirstNameSeller1", seller.FirstNameSeller1 ?? "");
                    cmd.Parameters.AddWithValue("@MobileSeller1", seller.MobileSeller1 ?? "");
                    cmd.Parameters.AddWithValue("@PhoneSeller1", seller.PhoneSeller1 ?? "");
                    cmd.Parameters.AddWithValue("@EmailSeller1", seller.EmailSeller1 ?? "");
                    cmd.Parameters.AddWithValue("@IDNumberSeller1", seller.IdSeller1 ?? "");
                    cmd.Parameters.AddWithValue("@PostAddressSeller1", seller.PostAddressSeller1 ?? "");

                    cmd.Parameters.AddWithValue("@SellerNumber2", seller.SellerNum2);
                    cmd.Parameters.AddWithValue("@LegalEntitySeller2", seller.LegalEntitySeller2 ?? "");
                    cmd.Parameters.AddWithValue("@RegistrationNumSeller2", seller.RegistrationNumberSeller2 ?? "");
                    cmd.Parameters.AddWithValue("@SurnameSeller2", seller.SurnameSeller2 ?? "");
                    cmd.Parameters.AddWithValue("@FirstNameSeller2", seller.FirstNameSeller2 ?? "");
                    cmd.Parameters.AddWithValue("@MobileSeller2", seller.MobileSeller2 ?? "");
                    cmd.Parameters.AddWithValue("@PhoneSeller2", seller.PhoneSeller2 ?? "");
                    cmd.Parameters.AddWithValue("@EmailSeller2", seller.EmailSeller2 ?? "");
                    cmd.Parameters.AddWithValue("@IDNumberSeller2", seller.IdSeller2 ?? "");
                    cmd.Parameters.AddWithValue("@PostAddressSeller2", seller.PostAddressSeller2 ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertOwnerDetails(Owner owner)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Owner_Details @ListingID, @OwnerNumber1, @LegalEntityOwner1, @RegistrationNumOwner1, @SurnameOwner1, @FirstNameOwner1,
                                    @MobileOwner1, @PhoneOwner1, @EmailOwner1, @IDNumberOwner1, @PostAddressOwner1,
                                    @OwnerNumber2, @LegalEntityOwner2, @RegistrationNumOwner2, @SurnameOwner2, @FirstNameOwner2,
                                    @MobileOwner2, @PhoneOwner2, @EmailOwner2, @IDNumberOwner2, @PostAddressOwner2 ";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingId", owner.ListingId);
                    cmd.Parameters.AddWithValue("@OwnerNumber1", owner.OwnerNum1);
                    cmd.Parameters.AddWithValue("@LegalEntityOwner1", owner.LegalEntityOwner1 ?? "");
                    cmd.Parameters.AddWithValue("@RegistrationNumOwner1", owner.RegistrationNumberOwner1 ?? "");
                    cmd.Parameters.AddWithValue("@SurnameOwner1", owner.SurnameOwner1 ?? "");
                    cmd.Parameters.AddWithValue("@FirstNameOwner1", owner.FirstNameOwner1 ?? "");
                    cmd.Parameters.AddWithValue("@MobileOwner1", owner.MobileOwner1 ?? "");
                    cmd.Parameters.AddWithValue("@PhoneOwner1", owner.PhoneOwner1 ?? "");
                    cmd.Parameters.AddWithValue("@EmailOwner1", owner.EmailOwner1 ?? "");
                    cmd.Parameters.AddWithValue("@IDNumberOwner1", owner.IdOwner1 ?? "");
                    cmd.Parameters.AddWithValue("@PostAddressOwner1", owner.PostAddressOwner1 ?? "");

                    cmd.Parameters.AddWithValue("@OwnerNumber2", owner.OwnerNum2);
                    cmd.Parameters.AddWithValue("@LegalEntityOwner2", owner.LegalEntityOwner2 ?? "");
                    cmd.Parameters.AddWithValue("@RegistrationNumOwner2", owner.RegistrationNumberOwner2 ?? "");
                    cmd.Parameters.AddWithValue("@SurnameOwner2", owner.SurnameOwner2 ?? "");
                    cmd.Parameters.AddWithValue("@FirstNameOwner2", owner.FirstNameOwner2 ?? "");
                    cmd.Parameters.AddWithValue("@MobileOwner2", owner.MobileOwner2 ?? "");
                    cmd.Parameters.AddWithValue("@PhoneOwner2", owner.PhoneOwner2 ?? "");
                    cmd.Parameters.AddWithValue("@EmailOwner2", owner.EmailOwner2 ?? "");
                    cmd.Parameters.AddWithValue("@IDNumberOwner2", owner.IdOwner2 ?? "");
                    cmd.Parameters.AddWithValue("@PostAddressOwner2", owner.PostAddressOwner2 ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertPrice(Price price)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Price @listingId, @MarketPrice, @NettPrice, @AgentFee, @Levies, @Rates, @WaterElectricity";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@listingId", price.ListingId);
                    cmd.Parameters.AddWithValue("@MarketPrice", price.MarketPrice);
                    cmd.Parameters.AddWithValue("@NettPrice", price.NettPrice);
                    cmd.Parameters.AddWithValue("@AgentFee", price.AgentFee);
                    cmd.Parameters.AddWithValue("@Levies", price.Levies);
                    cmd.Parameters.AddWithValue("@Rates", price.Rates);
                    cmd.Parameters.AddWithValue("@WaterElectricity", price.WaterElectricity);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertBuildingInformation(Building building)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                //string sqlQuery = @"EXEC fa_Insert_Building_Information @ListingID, @BuildingSqm, @StandSqm, @DateBuilt, @Facing";
                string sqlQuery = @"EXEC fa_Insert_Building_Information @ListingID, @BuildingSqm, @StandSqm, @Facing, @DateAvailable, @Furnished";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingID", building.ListingId);
                    cmd.Parameters.AddWithValue("@BuildingSqm", building.SqmBuilding);
                    cmd.Parameters.AddWithValue("@StandSqm", building.SqmStand);
                    //cmd.Parameters.AddWithValue("@DateBuilt", building.DateBuilt);
                    cmd.Parameters.AddWithValue("@Facing", building.Facing);
                    cmd.Parameters.AddWithValue("@DateAvailable", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Furnished", building.Furnished);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertStaffTenant(StaffTenant staffTenant)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Staff_Tenant @ListingID, @StaffName, @StaffMobile, @TenantName, @TenantMobile, @GrossRentalIncome, @DateRentalExpires, @DateAvailable";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingID", staffTenant.ListingId);
                    cmd.Parameters.AddWithValue("@StaffName", staffTenant.StaffName ?? "");
                    cmd.Parameters.AddWithValue("@StaffMobile", staffTenant.StaffMobile ?? "");
                    cmd.Parameters.AddWithValue("@TenantName", staffTenant.TenantName ?? "");
                    cmd.Parameters.AddWithValue("@TenantMobile", staffTenant.TenantMobile ?? "");
                    cmd.Parameters.AddWithValue("@GrossRentalIncome", staffTenant.GrossRentalIncome);
                    cmd.Parameters.AddWithValue("@DateRentalExpires", staffTenant.DateRentalExpires ?? "");
                    cmd.Parameters.AddWithValue("@DateAvailable", staffTenant.DateAvailable ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertFeatures(Features features)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Features @ListingId, @Office, @OpenPlan, @EntranceHall, @CoveredBalcony, @OpenBalcony, @Loft, @HomeAutomation, @View, @SolarHeating, 
	                                @RoofInsulation, @FireHearthGas, @FireHearthWood, @Safe, @StrongRoom, @StoreRoom, @PrepaidElectricity, @Generator, 
	                                @WiredForGenerator, @ADSL, @Fibre, @CentralVacuum, @HomeCinema, @SatelliteDish, @SolarGeyser, @OtherInformation, @NumberOfGeysers";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingID", features.ListingId);
                    cmd.Parameters.AddWithValue("@Office", features.Office);
                    cmd.Parameters.AddWithValue("@OpenPlan", features.OpenPlan);
                    cmd.Parameters.AddWithValue("@EntranceHall", features.EntranceHall);
                    cmd.Parameters.AddWithValue("@CoveredBalcony", features.CoveredBalcony);
                    cmd.Parameters.AddWithValue("@OpenBalcony", features.OpenPlan);
                    cmd.Parameters.AddWithValue("@Loft", features.Loft);
                    cmd.Parameters.AddWithValue("@HomeAutomation", features.HomeAutomation);
                    cmd.Parameters.AddWithValue("@View", features.View);
                    cmd.Parameters.AddWithValue("@SolarHeating", features.SolarHeating);
                    cmd.Parameters.AddWithValue("@RoofInsulation", features.RoofInsulation);
                    cmd.Parameters.AddWithValue("@FireHearthGas", features.FireHearthGas);
                    cmd.Parameters.AddWithValue("@FireHearthWood", features.FireHearthWood);
                    cmd.Parameters.AddWithValue("@Safe", features.Safe);
                    cmd.Parameters.AddWithValue("@StrongRoom", features.StrongRoom);
                    cmd.Parameters.AddWithValue("@StoreRoom", features.StoreRoom);
                    cmd.Parameters.AddWithValue("@PrepaidElectricity", features.PrepaidElectricity);
                    cmd.Parameters.AddWithValue("@Generator", features.Generator);
                    cmd.Parameters.AddWithValue("@WiredForGenerator", features.WiredForGenerator);
                    cmd.Parameters.AddWithValue("@ADSL", features.ADSL);
                    cmd.Parameters.AddWithValue("@Fibre", features.Fibre);
                    cmd.Parameters.AddWithValue("@CentralVacuum", features.CentralVacuum);
                    cmd.Parameters.AddWithValue("@HomeCinema", features.HomeCinema);
                    cmd.Parameters.AddWithValue("@SatelliteDish", features.SatelliteDish);
                    cmd.Parameters.AddWithValue("@SolarGeyser", features.SolarGeyser);
                    cmd.Parameters.AddWithValue("@OtherInformation", features.OtherInformation ?? "");
                    cmd.Parameters.AddWithValue("@NumberOfGeysers", features.NumberOfGeysers);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void InsertKitchenInformation(Kitchen kitchen)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Kitchen_Information @ListingID, @SeperateLaundry, @SeperateScullery, @WalkInPantry, @PantryCupboard, @KitchenComments, @Granite, @CaeserStone,
                                    @Melamine, @Wood, @TypeOfWood, @FinishesComments, @ElectricStove, @GasStove, @Hob, @ExtractorFan, @OvenUnderCounter, 
                                    @OvenEyeLevel, @Microwave, @CoffeeMachine, @DoubleDoorFridge, @TopLoader, @NumSpaces, @KitchenSpacesComments";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingID", kitchen.ListingId);
                    cmd.Parameters.AddWithValue("@SeperateLaundry", kitchen.SeperateLaundry);
                    cmd.Parameters.AddWithValue("@SeperateScullery", kitchen.SeperateScullery);
                    cmd.Parameters.AddWithValue("@WalkInPantry", kitchen.WalkInPantry);
                    cmd.Parameters.AddWithValue("@PantryCupboard", kitchen.PantryCupboard);
                    cmd.Parameters.AddWithValue("@KitchenComments", kitchen.KitchenComments);
                    cmd.Parameters.AddWithValue("@Granite", kitchen.Granite);
                    cmd.Parameters.AddWithValue("@CaeserStone", kitchen.CaeserStone);
                    cmd.Parameters.AddWithValue("@Melamine", kitchen.Melamine);
                    cmd.Parameters.AddWithValue("@Wood", kitchen.Wood);
                    cmd.Parameters.AddWithValue("@TypeOfWood", kitchen.TypeOfWood ?? "");
                    cmd.Parameters.AddWithValue("@FinishesComments", kitchen.FinishesComments ?? "");
                    cmd.Parameters.AddWithValue("@ElectricStove", kitchen.ElectricStove);
                    cmd.Parameters.AddWithValue("@GasStove", kitchen.GasStove);
                    cmd.Parameters.AddWithValue("@Hob", kitchen.Hob);
                    cmd.Parameters.AddWithValue("@ExtractorFan", kitchen.ExtractorFan);
                    cmd.Parameters.AddWithValue("@OvenUnderCounter", kitchen.OvenUnderCounter);
                    cmd.Parameters.AddWithValue("@OvenEyeLevel", kitchen.OvenEyeLevel);
                    cmd.Parameters.AddWithValue("@Microwave", kitchen.Microwave);
                    cmd.Parameters.AddWithValue("@CoffeeMachine", kitchen.CoffeeMachine);
                    cmd.Parameters.AddWithValue("@DoubleDoorFridge", kitchen.DoubleDoorFridge);
                    cmd.Parameters.AddWithValue("@TopLoader", kitchen.TopLoader);
                    cmd.Parameters.AddWithValue("@NumSpaces", kitchen.NumSpaces);
                    cmd.Parameters.AddWithValue("@KitchenSpacesComments", kitchen.KitchenSpacesComments ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertEntertainmentInformation(Entertainment entertainment)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Entertainment @ListingID,  @CoveredPatio, @EnclosedPatio, @Boma, @GasBarbeque, @WoodBarbeque, @Bar, @WineCellar, 
                                        @Jacuzzi, @Sauna, @Gym, @Pool, @EntertainmentComments";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingID", entertainment.ListingId);
                    cmd.Parameters.AddWithValue("@CoveredPatio", entertainment.CoveredPatio);
                    cmd.Parameters.AddWithValue("@EnclosedPatio", entertainment.EnclosedPatio);
                    cmd.Parameters.AddWithValue("@Boma", entertainment.Boma);
                    cmd.Parameters.AddWithValue("@GasBarbeque", entertainment.GasBarbeque);
                    cmd.Parameters.AddWithValue("@WoodBarbeque", entertainment.WoodBarbeque);
                    cmd.Parameters.AddWithValue("@Bar", entertainment.Bar);
                    cmd.Parameters.AddWithValue("@WineCellar", entertainment.WineCellar);
                    cmd.Parameters.AddWithValue("@Jacuzzi", entertainment.Jacuzzi);
                    cmd.Parameters.AddWithValue("@Sauna", entertainment.Sauna);
                    cmd.Parameters.AddWithValue("@Gym", entertainment.Gym);
                    cmd.Parameters.AddWithValue("@Pool", entertainment.Pool);
                    cmd.Parameters.AddWithValue("@EntertainmentComments", entertainment.EntertainmentComments ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void InsertSecurityInformation(Security security)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Security_Information @ListingID, @Alarm, @BurglarProofing, @SecurityGates, @PassiveBeams, @Intercom, @SecurityComments";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingID", security.ListingId);
                    cmd.Parameters.AddWithValue("@Alarm", security.Alarm);
                    cmd.Parameters.AddWithValue("@BurglarProofing", security.BurglarProofing);
                    cmd.Parameters.AddWithValue("@SecurityGates", security.SecurityGates);
                    cmd.Parameters.AddWithValue("@PassiveBeams", security.PassiveBeams);
                    cmd.Parameters.AddWithValue("@Intercom", security.Intercom);
                    cmd.Parameters.AddWithValue("@SecurityComments", security.SecurityComments ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertGarageInformation(Garage garage)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Garage_Information @ListingID, @AutomatedGarages, @NumberGarages, @NumberAutomatedGarages, @NumberGarageMotors";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingID", garage.ListingId);
                    cmd.Parameters.AddWithValue("@AutomatedGarages", garage.AutomatedGarages);
                    cmd.Parameters.AddWithValue("@NumberGarages", garage.NumberOfGarages);
                    cmd.Parameters.AddWithValue("@NumberAutomatedGarages", garage.NumberOfGaragesAutomated);
                    cmd.Parameters.AddWithValue("@NumberGarageMotors", garage.NumberGarageMotors);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertWallInformation(Wall wall)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Wall_Information @ListingID, @Facebrick, @Plaster";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingID", wall.ListingId);
                    cmd.Parameters.AddWithValue("@Facebrick", wall.Facebrick);
                    cmd.Parameters.AddWithValue("@Plaster", wall.Plaster);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void InsertWindowInformation(Window window)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Window_Information @ListingID, @WindowSteel, @WindowWood, @WindowAluminium";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingID", window.ListingId);
                    cmd.Parameters.AddWithValue("@WindowSteel", window.Steel);
                    cmd.Parameters.AddWithValue("@WindowWood", window.Wood);
                    cmd.Parameters.AddWithValue("@WindowAluminium", window.Aluminium);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertRoofInformation(Roof roof)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Roof_Information @ListingID, @RoofTiled, @RoofChromadech, @RoofThatch, @RoofSlate, @RoofSlab, @RoofFlat, @RoofPitch";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingID", roof.ListingId);
                    cmd.Parameters.AddWithValue("@RoofTiled", roof.Tiled);
                    cmd.Parameters.AddWithValue("@RoofChromadech", roof.Chromadech);
                    cmd.Parameters.AddWithValue("@RoofThatch", roof.Thatch);
                    cmd.Parameters.AddWithValue("@RoofSlate", roof.Slate);
                    cmd.Parameters.AddWithValue("@RoofSlab", roof.Slab);
                    cmd.Parameters.AddWithValue("@RoofFlat", roof.Flat);
                    cmd.Parameters.AddWithValue("@RoofPitch", roof.Pitch);

                    cmd.ExecuteNonQuery();

                }
            }
        }
        public void InsertCeilingInformation(Ceiling ceiling)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Ceiling_Information @ListingID, @CeilingRhinolite, @CeilingGypsum";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingID", ceiling.ListingId);
                    cmd.Parameters.AddWithValue("@CeilingRhinolite", ceiling.Rhinolite);
                    cmd.Parameters.AddWithValue("@CeilingGypsum", ceiling.Gypsum);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertGardenInformation(Garden garden)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Garden_Information @ListingId, @GardenIrrigation, @GardenComputerized, @GardenBorehole, @GardenJojoTanks, @GardenWaterFeature";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingId", garden.ListingId);
                    cmd.Parameters.AddWithValue("@GardenIrrigation", garden.Irrigation);
                    cmd.Parameters.AddWithValue("@GardenComputerized", garden.Computerized);
                    cmd.Parameters.AddWithValue("@GardenBorehole", garden.Borehole);
                    cmd.Parameters.AddWithValue("@GardenJojoTanks", garden.JojoTanks);
                    cmd.Parameters.AddWithValue("@GardenWaterFeature", garden.WaterFeature);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertPoolInformation(Pool pool)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Pool_Information @ListingId, @SaltChlorinated, @StandardChemicals, @Cleaner, @Cover, @Net, @ElectricHeated, 
                                    @SolarHeated, @PoolFence, @FibreGlass, @Marbelite, @Gunite, @Comments";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingId", pool.ListingId);
                    cmd.Parameters.AddWithValue("@SaltChlorinated", pool.SaltChlorinated);
                    cmd.Parameters.AddWithValue("@StandardChemicals", pool.StandardChemicals);
                    cmd.Parameters.AddWithValue("@Cleaner", pool.Cleaner);
                    cmd.Parameters.AddWithValue("@Cover", pool.Cover);
                    cmd.Parameters.AddWithValue("@Net", pool.Net);
                    cmd.Parameters.AddWithValue("@ElectricHeated", pool.ElectricHeated);
                    cmd.Parameters.AddWithValue("@SolarHeated", pool.SolarHeated);
                    cmd.Parameters.AddWithValue("@PoolFence", pool.PoolFence);
                    cmd.Parameters.AddWithValue("@FibreGlass", pool.FibreGlass);
                    cmd.Parameters.AddWithValue("@Marbelite", pool.Marbelite);
                    cmd.Parameters.AddWithValue("@Gunite", pool.Gunite);
                    cmd.Parameters.AddWithValue("@Comments", pool.PoolComments ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertBoundryInformation(Boundry boundry)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = "EXEC fa_Insert_Boundry_Information @ListingId, @BrickWall,  @Palisade, @Hedge, @Vibracrete";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingId", boundry.ListingId);
                    cmd.Parameters.AddWithValue("@BrickWall", boundry.BrickWall);
                    cmd.Parameters.AddWithValue("@Palisade", boundry.Palisade);
                    cmd.Parameters.AddWithValue("@Hedge", boundry.Hedge);
                    cmd.Parameters.AddWithValue("@Vibracrete", boundry.Vibracrete);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertDefectsInformation(Defects defects)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Defects_Information @ListingId, @DefectsCracks, @DefectsLeaks, @DefectsDamp, @DefectsElectricity, @DefectsPlumbing, 
                                    @DefectsNoneVisible, @DefectsOther, @DefectsComments";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingId", defects.ListingId);
                    cmd.Parameters.AddWithValue("@DefectsCracks", defects.Cracks);
                    cmd.Parameters.AddWithValue("@DefectsLeaks", defects.Leaks);
                    cmd.Parameters.AddWithValue("@DefectsDamp", defects.Damp);
                    cmd.Parameters.AddWithValue("@DefectsElectricity", defects.Electricity);
                    cmd.Parameters.AddWithValue("@DefectsPlumbing", defects.Plumbing);
                    cmd.Parameters.AddWithValue("@DefectsNoneVisible", defects.NoneVisible);
                    cmd.Parameters.AddWithValue("@DefectsOther", defects.Other);
                    cmd.Parameters.AddWithValue("@DefectsComments", defects.DefectsComments ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertExtraAccommodationInformation(ExtraAccommodation extraAcommodation)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Extra_Accommodation_information @ListingID, @ExtraToilet, @ExtraHandWashBasin,  @ExtraShower, @ExtraBath,
                                        @ExtraBedroom, @ExtraKitchen, @ExtraLivingArea, @ExtraGarage, @ExtraCoveredParkingBay, @StaffToilet,  @StaffHandWashBasin, 
                                        @StaffShower,  @StaffBath, @StaffBedroom, @StaffKitchen, @StaffLivingArea, @StaffGarage, @StaffCoveredParkingBay, 
                                        @ExtraAccommodationComments";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingID", extraAcommodation.ListingId);
                    cmd.Parameters.AddWithValue("@ExtraToilet", extraAcommodation.ExtraToilet);
                    cmd.Parameters.AddWithValue("@ExtraHandWashBasin", extraAcommodation.ExtraHandWashBasin);
                    cmd.Parameters.AddWithValue("@ExtraShower", extraAcommodation.ExtraShower);
                    cmd.Parameters.AddWithValue("@ExtraBath", extraAcommodation.ExtraBath);
                    cmd.Parameters.AddWithValue("@ExtraBedroom", extraAcommodation.ExtraBedroom);
                    cmd.Parameters.AddWithValue("@ExtraKitchen", extraAcommodation.ExtraKitchen);
                    cmd.Parameters.AddWithValue("@ExtraLivingArea", extraAcommodation.ExtraLivingArea);
                    cmd.Parameters.AddWithValue("@ExtraGarage", extraAcommodation.ExtraGarage);
                    cmd.Parameters.AddWithValue("@ExtraCoveredParkingBay", extraAcommodation.ExtraCoveredParkingBay);
                    cmd.Parameters.AddWithValue("@StaffToilet", extraAcommodation.StaffToilet);
                    cmd.Parameters.AddWithValue("@StaffHandWashBasin", extraAcommodation.StaffHandWashBasin);
                    cmd.Parameters.AddWithValue("@StaffShower", extraAcommodation.StaffShower);
                    cmd.Parameters.AddWithValue("@StaffBath", extraAcommodation.StaffBath);
                    cmd.Parameters.AddWithValue("@StaffBedroom", extraAcommodation.StaffBedroom);
                    cmd.Parameters.AddWithValue("@StaffKitchen", extraAcommodation.StaffKitchen);
                    cmd.Parameters.AddWithValue("@StaffLivingArea", extraAcommodation.StaffLivingArea);
                    cmd.Parameters.AddWithValue("@StaffGarage", extraAcommodation.StaffGarage);
                    cmd.Parameters.AddWithValue("@StaffCoveredParkingBay", extraAcommodation.StaffCoveredParkingBay);
                    cmd.Parameters.AddWithValue("@ExtraAccommodationComments", extraAcommodation.ExtraAccommodationComments ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertLivingAreasInformation(LivingArea livingAreas)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Living_Areas 
                                            @ListingId,
                                            @LivingArea1TypeComments,
	                                        @LivingArea1Tiled,
	                                        @LivingArea1Wood,
	                                        @LivingArea1Carpet,
	                                        @LivingArea1UnderFloorHeating,
	                                        @LivingArea1FirePlaceGas,
	                                        @LivingArea1FirePlaceWood,
	                                        @LivingArea1AirConditioning,
	                                        @LivingArea1Fan,
	                                        @LivingArea1SurroundSound,
	                                        @LivingArea1Upstairs,
	                                        @LivingArea1Downstairs,
	                                        @LivingArea1Comments,
                                            @LivingArea2TypeComments,
	                                        @LivingArea2Tiled,
	                                        @LivingArea2Wood,
	                                        @LivingArea2Carpet,
	                                        @LivingArea2UnderFloorHeating,
	                                        @LivingArea2FirePlaceGas,
	                                        @LivingArea2FirePlaceWood,
	                                        @LivingArea2AirConditioning,
	                                        @LivingArea2Fan,
	                                        @LivingArea2SurroundSound,
	                                        @LivingArea2Upstairs,
	                                        @LivingArea2Downstairs,
	                                        @LivingArea2Comments,
                                            @LivingArea3TypeComments,
	                                        @LivingArea3Tiled,
	                                        @LivingArea3Wood,
	                                        @LivingArea3Carpet,
	                                        @LivingArea3UnderFloorHeating,
	                                        @LivingArea3FirePlaceGas,
	                                        @LivingArea3FirePlaceWood,
	                                        @LivingArea3AirConditioning,
	                                        @LivingArea3Fan,
	                                        @LivingArea3SurroundSound,
	                                        @LivingArea3Upstairs,
	                                        @LivingArea3Downstairs,
	                                        @LivingArea3Comments,
                                            @LivingArea4TypeComments,
	                                        @LivingArea4Tiled,
	                                        @LivingArea4Wood,
	                                        @LivingArea4Carpet,
	                                        @LivingArea4UnderFloorHeating,
	                                        @LivingArea4FirePlaceGas,
	                                        @LivingArea4FirePlaceWood,
	                                        @LivingArea4AirConditioning,
	                                        @LivingArea4Fan,
	                                        @LivingArea4SurroundSound,
	                                        @LivingArea4Upstairs,
	                                        @LivingArea4Downstairs,
	                                        @LivingArea4Comments,
                                            @LivingArea5TypeComments,
	                                        @LivingArea5Tiled,
	                                        @LivingArea5Wood,
	                                        @LivingArea5Carpet,
	                                        @LivingArea5UnderFloorHeating,
	                                        @LivingArea5FirePlaceGas,
	                                        @LivingArea5FirePlaceWood,
	                                        @LivingArea5AirConditioning,
	                                        @LivingArea5Fan,
	                                        @LivingArea5SurroundSound,
	                                        @LivingArea5Upstairs,
	                                        @LivingArea5Downstairs,
	                                        @LivingArea5Comments,
                                            @LivingAreaStudyTypeComments,
	                                        @LivingAreaStudyTiled,
	                                        @LivingAreaStudyWood,
	                                        @LivingAreaStudyCarpet,
	                                        @LivingAreaStudyUnderFloorHeating,
	                                        @LivingAreaStudyFirePlaceGas,
	                                        @LivingAreaStudyFirePlaceWood,
	                                        @LivingAreaStudyAirConditioning,
	                                        @LivingAreaStudyFan,
	                                        @LivingAreaStudySurroundSound,
	                                        @LivingAreaStudyUpstairs,
	                                        @LivingAreaStudyDownstairs,
	                                        @LivingAreaStudyComments";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingId", livingAreas.ListingId);
                    cmd.Parameters.AddWithValue("@LivingArea1TypeComments", livingAreas.LivingArea1TypeComments ?? "");
                    cmd.Parameters.AddWithValue("@LivingArea1Tiled", livingAreas.LivingArea1Tiled);
                    cmd.Parameters.AddWithValue("@LivingArea1Wood", livingAreas.LivingArea1Wood);
                    cmd.Parameters.AddWithValue("@LivingArea1Carpet", livingAreas.LivingArea1Carpet);
                    cmd.Parameters.AddWithValue("@LivingArea1UnderFloorHeating", livingAreas.LivingArea1UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@LivingArea1FirePlaceGas", livingAreas.LivingArea1FirePlaceGas);
                    cmd.Parameters.AddWithValue("@LivingArea1FirePlaceWood", livingAreas.LivingArea1FirePlaceWood);
                    cmd.Parameters.AddWithValue("@LivingArea1AirConditioning", livingAreas.LivingArea1AirConditioning);
                    cmd.Parameters.AddWithValue("@LivingArea1Fan", livingAreas.LivingArea1Fan);
                    cmd.Parameters.AddWithValue("@LivingArea1SurroundSound", livingAreas.LivingArea1SurroundSound);
                    cmd.Parameters.AddWithValue("@LivingArea1Upstairs", livingAreas.LivingArea1Upstairs);
                    cmd.Parameters.AddWithValue("@LivingArea1Downstairs", livingAreas.LivingArea1Downstairs);
                    cmd.Parameters.AddWithValue("@LivingArea1Comments", livingAreas.LivingArea1Comments ?? "");

                    cmd.Parameters.AddWithValue("@LivingArea2TypeComments", livingAreas.LivingArea2TypeComments ?? "");
                    cmd.Parameters.AddWithValue("@LivingArea2Tiled", livingAreas.LivingArea2Tiled);
                    cmd.Parameters.AddWithValue("@LivingArea2Wood", livingAreas.LivingArea2Wood);
                    cmd.Parameters.AddWithValue("@LivingArea2Carpet", livingAreas.LivingArea2Carpet);
                    cmd.Parameters.AddWithValue("@LivingArea2UnderFloorHeating", livingAreas.LivingArea2UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@LivingArea2FirePlaceGas", livingAreas.LivingArea2FirePlaceGas);
                    cmd.Parameters.AddWithValue("@LivingArea2FirePlaceWood", livingAreas.LivingArea2FirePlaceWood);
                    cmd.Parameters.AddWithValue("@LivingArea2AirConditioning", livingAreas.LivingArea2AirConditioning);
                    cmd.Parameters.AddWithValue("@LivingArea2Fan", livingAreas.LivingArea2Fan);
                    cmd.Parameters.AddWithValue("@LivingArea2SurroundSound", livingAreas.LivingArea2SurroundSound);
                    cmd.Parameters.AddWithValue("@LivingArea2Upstairs", livingAreas.LivingArea2Upstairs);
                    cmd.Parameters.AddWithValue("@LivingArea2Downstairs", livingAreas.LivingArea2Downstairs);
                    cmd.Parameters.AddWithValue("@LivingArea2Comments", livingAreas.LivingArea2Comments ?? "");

                    cmd.Parameters.AddWithValue("@LivingArea3TypeComments", livingAreas.LivingArea3TypeComments ?? "");
                    cmd.Parameters.AddWithValue("@LivingArea3Tiled", livingAreas.LivingArea3Tiled);
                    cmd.Parameters.AddWithValue("@LivingArea3Wood", livingAreas.LivingArea3Wood);
                    cmd.Parameters.AddWithValue("@LivingArea3Carpet", livingAreas.LivingArea3Carpet);
                    cmd.Parameters.AddWithValue("@LivingArea3UnderFloorHeating", livingAreas.LivingArea3UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@LivingArea3FirePlaceGas", livingAreas.LivingArea3FirePlaceGas);
                    cmd.Parameters.AddWithValue("@LivingArea3FirePlaceWood", livingAreas.LivingArea3FirePlaceWood);
                    cmd.Parameters.AddWithValue("@LivingArea3AirConditioning", livingAreas.LivingArea3AirConditioning);
                    cmd.Parameters.AddWithValue("@LivingArea3Fan", livingAreas.LivingArea3Fan);
                    cmd.Parameters.AddWithValue("@LivingArea3SurroundSound", livingAreas.LivingArea3SurroundSound);
                    cmd.Parameters.AddWithValue("@LivingArea3Upstairs", livingAreas.LivingArea3Upstairs);
                    cmd.Parameters.AddWithValue("@LivingArea3Downstairs", livingAreas.LivingArea3Downstairs);
                    cmd.Parameters.AddWithValue("@LivingArea3Comments", livingAreas.LivingArea3Comments ?? "");

                    cmd.Parameters.AddWithValue("@LivingArea4TypeComments", livingAreas.LivingArea4TypeComments ?? "");
                    cmd.Parameters.AddWithValue("@LivingArea4Tiled", livingAreas.LivingArea4Tiled);
                    cmd.Parameters.AddWithValue("@LivingArea4Wood", livingAreas.LivingArea4Wood);
                    cmd.Parameters.AddWithValue("@LivingArea4Carpet", livingAreas.LivingArea4Carpet);
                    cmd.Parameters.AddWithValue("@LivingArea4UnderFloorHeating", livingAreas.LivingArea4UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@LivingArea4FirePlaceGas", livingAreas.LivingArea4FirePlaceGas);
                    cmd.Parameters.AddWithValue("@LivingArea4FirePlaceWood", livingAreas.LivingArea4FirePlaceWood);
                    cmd.Parameters.AddWithValue("@LivingArea4AirConditioning", livingAreas.LivingArea4AirConditioning);
                    cmd.Parameters.AddWithValue("@LivingArea4Fan", livingAreas.LivingArea4Fan);
                    cmd.Parameters.AddWithValue("@LivingArea4SurroundSound", livingAreas.LivingArea4SurroundSound);
                    cmd.Parameters.AddWithValue("@LivingArea4Upstairs", livingAreas.LivingArea4Upstairs);
                    cmd.Parameters.AddWithValue("@LivingArea4Downstairs", livingAreas.LivingArea4Downstairs);
                    cmd.Parameters.AddWithValue("@LivingArea4Comments", livingAreas.LivingArea4Comments ?? "");

                    cmd.Parameters.AddWithValue("@LivingArea5TypeComments", livingAreas.LivingArea5TypeComments ?? "");
                    cmd.Parameters.AddWithValue("@LivingArea5Tiled", livingAreas.LivingArea5Tiled);
                    cmd.Parameters.AddWithValue("@LivingArea5Wood", livingAreas.LivingArea5Wood);
                    cmd.Parameters.AddWithValue("@LivingArea5Carpet", livingAreas.LivingArea5Carpet);
                    cmd.Parameters.AddWithValue("@LivingArea5UnderFloorHeating", livingAreas.LivingArea5UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@LivingArea5FirePlaceGas", livingAreas.LivingArea5FirePlaceGas);
                    cmd.Parameters.AddWithValue("@LivingArea5FirePlaceWood", livingAreas.LivingArea5FirePlaceWood);
                    cmd.Parameters.AddWithValue("@LivingArea5AirConditioning", livingAreas.LivingArea5AirConditioning);
                    cmd.Parameters.AddWithValue("@LivingArea5Fan", livingAreas.LivingArea5Fan);
                    cmd.Parameters.AddWithValue("@LivingArea5SurroundSound", livingAreas.LivingArea5SurroundSound);
                    cmd.Parameters.AddWithValue("@LivingArea5Upstairs", livingAreas.LivingArea5Upstairs);
                    cmd.Parameters.AddWithValue("@LivingArea5Downstairs", livingAreas.LivingArea5Downstairs);
                    cmd.Parameters.AddWithValue("@LivingArea5Comments", livingAreas.LivingArea5Comments ?? "");

                    cmd.Parameters.AddWithValue("@LivingAreaStudyTypeComments", livingAreas.LivingAreaStudyTypeComments ?? "");
                    cmd.Parameters.AddWithValue("@LivingAreaStudyTiled", livingAreas.LivingAreaStudyTiled);
                    cmd.Parameters.AddWithValue("@LivingAreaStudyWood", livingAreas.LivingAreaStudyWood);
                    cmd.Parameters.AddWithValue("@LivingAreaStudyCarpet", livingAreas.LivingAreaStudyCarpet);
                    cmd.Parameters.AddWithValue("@LivingAreaStudyUnderFloorHeating", livingAreas.LivingAreaStudyUnderFloorHeating);
                    cmd.Parameters.AddWithValue("@LivingAreaStudyFirePlaceGas", livingAreas.LivingAreaStudyFirePlaceGas);
                    cmd.Parameters.AddWithValue("@LivingAreaStudyFirePlaceWood", livingAreas.LivingAreaStudyFirePlaceWood);
                    cmd.Parameters.AddWithValue("@LivingAreaStudyAirConditioning", livingAreas.LivingAreaStudyAirConditioning);
                    cmd.Parameters.AddWithValue("@LivingAreaStudyFan", livingAreas.LivingAreaStudyFan);
                    cmd.Parameters.AddWithValue("@LivingAreaStudySurroundSound", livingAreas.LivingAreaStudySurroundSound);
                    cmd.Parameters.AddWithValue("@LivingAreaStudyUpstairs", livingAreas.LivingAreaStudyUpstairs);
                    cmd.Parameters.AddWithValue("@LivingAreaStudyDownstairs", livingAreas.LivingAreaStudyDownstairs);
                    cmd.Parameters.AddWithValue("@LivingAreaStudyComments", livingAreas.LivingAreaStudyComments ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertBedroomsInformation(Bedrooms bedrooms)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Bedroom_Information @ListingId,
	                                    @BedroomMainTiled,
	                                    @BedroomMainWood,
	                                    @BedroomMainCarpet,
	                                    @BedroomMainUnderFloorHeating,
	                                    @BedroomMainAirConditioning,
	                                    @BedroomMainFan,
	                                    @BedroomMainBuiltInWardrobe,
	                                    @BedroomMainWalkInCloset,
	                                    @BedroomMainEnSuite,
	                                    @BedroomMainSurroundSound,
	                                    @BedroomMainFirePlaceGas,
	                                    @BedroomMainFirePlaceWood,
	                                    @BedroomMainUpstairs,
	                                    @BedroomMainDownstairs,
	                                    @BedroomMainComments,

	                                    @Bedroom2Tiled,
	                                    @Bedroom2Wood,
	                                    @Bedroom2Carpet,
	                                    @Bedroom2UnderFloorHeating,
	                                    @Bedroom2AirConditioning,
	                                    @Bedroom2Fan,
	                                    @Bedroom2BuiltInWardrobe,
	                                    @Bedroom2WalkInCloset,
	                                    @Bedroom2EnSuite,
	                                    @Bedroom2SurroundSound,
	                                    @Bedroom2FirePlaceGas,
	                                    @Bedroom2FirePlaceWood,
	                                    @Bedroom2Upstairs,
	                                    @Bedroom2Downstairs,
	                                    @Bedroom2Comments,

	                                    @Bedroom3Tiled,
	                                    @Bedroom3Wood,
	                                    @Bedroom3Carpet,
	                                    @Bedroom3UnderFloorHeating,
	                                    @Bedroom3AirConditioning,
	                                    @Bedroom3Fan,
	                                    @Bedroom3BuiltInWardrobe,
	                                    @Bedroom3WalkInCloset,
	                                    @Bedroom3EnSuite,
	                                    @Bedroom3SurroundSound,
	                                    @Bedroom3FirePlaceGas,
	                                    @Bedroom3FirePlaceWood,
	                                    @Bedroom3Upstairs,
	                                    @Bedroom3Downstairs,
	                                    @Bedroom3Comments,

	                                    @Bedroom4Tiled,
	                                    @Bedroom4Wood,
	                                    @Bedroom4Carpet,
	                                    @Bedroom4UnderFloorHeating,
	                                    @Bedroom4AirConditioning,
	                                    @Bedroom4Fan,
	                                    @Bedroom4BuiltInWardrobe,
	                                    @Bedroom4WalkInCloset,
	                                    @Bedroom4EnSuite,
	                                    @Bedroom4SurroundSound,
	                                    @Bedroom4FirePlaceGas,
	                                    @Bedroom4FirePlaceWood,
	                                    @Bedroom4Upstairs,
	                                    @Bedroom4Downstairs,
	                                    @Bedroom4Comments,

	                                    @Bedroom5Tiled,
	                                    @Bedroom5Wood,
	                                    @Bedroom5Carpet,
	                                    @Bedroom5UnderFloorHeating,
	                                    @Bedroom5AirConditioning,
	                                    @Bedroom5Fan,
	                                    @Bedroom5BuiltInWardrobe,
	                                    @Bedroom5WalkInCloset,
	                                    @Bedroom5EnSuite,
	                                    @Bedroom5SurroundSound,
	                                    @Bedroom5FirePlaceGas,
	                                    @Bedroom5FirePlaceWood,
	                                    @Bedroom5Upstairs,
	                                    @Bedroom5Downstairs,
	                                    @Bedroom5Comments,

	                                    @Bedroom6Tiled,
	                                    @Bedroom6Wood,
	                                    @Bedroom6Carpet,
	                                    @Bedroom6UnderFloorHeating,
	                                    @Bedroom6AirConditioning,
	                                    @Bedroom6Fan,
	                                    @Bedroom6BuiltInWardrobe,
	                                    @Bedroom6WalkInCloset,
	                                    @Bedroom6EnSuite,
	                                    @Bedroom6SurroundSound,
	                                    @Bedroom6FirePlaceGas,
	                                    @Bedroom6FirePlaceWood,
	                                    @Bedroom6Upstairs,
	                                    @Bedroom6Downstairs,
	                                    @Bedroom6Comments";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingId", bedrooms.ListingId);
                    cmd.Parameters.AddWithValue("@BedroomMainTiled", bedrooms.BedroomMainTiled);
                    cmd.Parameters.AddWithValue("@BedroomMainWood", bedrooms.BedroomMainWood);
                    cmd.Parameters.AddWithValue("@BedroomMainCarpet", bedrooms.BedroomMainCarpet);
                    cmd.Parameters.AddWithValue("@BedroomMainUnderFloorHeating", bedrooms.BedroomMainUnderFloorHeating);
                    cmd.Parameters.AddWithValue("@BedroomMainAirConditioning", bedrooms.BedroomMainAirConditioning);
                    cmd.Parameters.AddWithValue("@BedroomMainFan", bedrooms.BedroomMainFan);
                    cmd.Parameters.AddWithValue("@BedroomMainBuiltInWardrobe", bedrooms.BedroomMainBuiltInWardrobe);
                    cmd.Parameters.AddWithValue("@BedroomMainWalkInCloset", bedrooms.BedroomMainWalkInCloset);
                    cmd.Parameters.AddWithValue("@BedroomMainEnSuite", bedrooms.BedroomMainEnSuite);
                    cmd.Parameters.AddWithValue("@BedroomMainSurroundSound", bedrooms.BedroomMainSurroundSound);
                    cmd.Parameters.AddWithValue("@BedroomMainFirePlaceGas", bedrooms.BedroomMainFirePlaceGas);
                    cmd.Parameters.AddWithValue("@BedroomMainFirePlaceWood", bedrooms.BedroomMainFirePlaceWood);
                    cmd.Parameters.AddWithValue("@BedroomMainUpstairs", bedrooms.BedroomMainUpstairs);
                    cmd.Parameters.AddWithValue("@BedroomMainDownstairs", bedrooms.BedroomMainDownstairs);
                    cmd.Parameters.AddWithValue("@BedroomMainComments", bedrooms.BedroomMainComments ?? "");


                    cmd.Parameters.AddWithValue("@Bedroom2Tiled", bedrooms.Bedroom2Tiled);
                    cmd.Parameters.AddWithValue("@Bedroom2Wood", bedrooms.Bedroom2Wood);
                    cmd.Parameters.AddWithValue("@Bedroom2Carpet", bedrooms.Bedroom2Carpet);
                    cmd.Parameters.AddWithValue("@Bedroom2UnderFloorHeating", bedrooms.Bedroom2UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@Bedroom2AirConditioning", bedrooms.Bedroom2AirConditioning);
                    cmd.Parameters.AddWithValue("@Bedroom2Fan", bedrooms.Bedroom2Fan);
                    cmd.Parameters.AddWithValue("@Bedroom2BuiltInWardrobe", bedrooms.Bedroom2BuiltInWardrobe);
                    cmd.Parameters.AddWithValue("@Bedroom2WalkInCloset", bedrooms.Bedroom2WalkInCloset);
                    cmd.Parameters.AddWithValue("@Bedroom2EnSuite", bedrooms.Bedroom2EnSuite);
                    cmd.Parameters.AddWithValue("@Bedroom2SurroundSound", bedrooms.Bedroom2SurroundSound);
                    cmd.Parameters.AddWithValue("@Bedroom2FirePlaceGas", bedrooms.Bedroom2FirePlaceGas);
                    cmd.Parameters.AddWithValue("@Bedroom2FirePlaceWood", bedrooms.Bedroom2FirePlaceWood);
                    cmd.Parameters.AddWithValue("@Bedroom2Upstairs", bedrooms.Bedroom2Upstairs);
                    cmd.Parameters.AddWithValue("@Bedroom2Downstairs", bedrooms.Bedroom2Downstairs);
                    cmd.Parameters.AddWithValue("@Bedroom2Comments", bedrooms.Bedroom2Comments ?? "");


                    cmd.Parameters.AddWithValue("@Bedroom3Tiled", bedrooms.Bedroom3Tiled);
                    cmd.Parameters.AddWithValue("@Bedroom3Wood", bedrooms.Bedroom3Wood);
                    cmd.Parameters.AddWithValue("@Bedroom3Carpet", bedrooms.Bedroom3Carpet);
                    cmd.Parameters.AddWithValue("@Bedroom3UnderFloorHeating", bedrooms.Bedroom3UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@Bedroom3AirConditioning", bedrooms.Bedroom3AirConditioning);
                    cmd.Parameters.AddWithValue("@Bedroom3Fan", bedrooms.Bedroom3Fan);
                    cmd.Parameters.AddWithValue("@Bedroom3BuiltInWardrobe", bedrooms.Bedroom3BuiltInWardrobe);
                    cmd.Parameters.AddWithValue("@Bedroom3WalkInCloset", bedrooms.Bedroom3WalkInCloset);
                    cmd.Parameters.AddWithValue("@Bedroom3EnSuite", bedrooms.Bedroom3EnSuite);
                    cmd.Parameters.AddWithValue("@Bedroom3SurroundSound", bedrooms.Bedroom3SurroundSound);
                    cmd.Parameters.AddWithValue("@Bedroom3FirePlaceGas", bedrooms.Bedroom3FirePlaceGas);
                    cmd.Parameters.AddWithValue("@Bedroom3FirePlaceWood", bedrooms.Bedroom3FirePlaceWood);
                    cmd.Parameters.AddWithValue("@Bedroom3Upstairs", bedrooms.Bedroom3Upstairs);
                    cmd.Parameters.AddWithValue("@Bedroom3Downstairs", bedrooms.Bedroom3Downstairs);
                    cmd.Parameters.AddWithValue("@Bedroom3Comments", bedrooms.Bedroom3Comments ?? "");

                    cmd.Parameters.AddWithValue("@Bedroom4Tiled", bedrooms.Bedroom4Tiled);
                    cmd.Parameters.AddWithValue("@Bedroom4Wood", bedrooms.Bedroom4Wood);
                    cmd.Parameters.AddWithValue("@Bedroom4Carpet", bedrooms.Bedroom4Carpet);
                    cmd.Parameters.AddWithValue("@Bedroom4UnderFloorHeating", bedrooms.Bedroom4UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@Bedroom4AirConditioning", bedrooms.Bedroom4AirConditioning);
                    cmd.Parameters.AddWithValue("@Bedroom4Fan", bedrooms.Bedroom4Fan);
                    cmd.Parameters.AddWithValue("@Bedroom4BuiltInWardrobe", bedrooms.Bedroom4BuiltInWardrobe);
                    cmd.Parameters.AddWithValue("@Bedroom4WalkInCloset", bedrooms.Bedroom4WalkInCloset);
                    cmd.Parameters.AddWithValue("@Bedroom4EnSuite", bedrooms.Bedroom4EnSuite);
                    cmd.Parameters.AddWithValue("@Bedroom4SurroundSound", bedrooms.Bedroom4SurroundSound);
                    cmd.Parameters.AddWithValue("@Bedroom4FirePlaceGas", bedrooms.Bedroom4FirePlaceGas);
                    cmd.Parameters.AddWithValue("@Bedroom4FirePlaceWood", bedrooms.Bedroom4FirePlaceWood);
                    cmd.Parameters.AddWithValue("@Bedroom4Upstairs", bedrooms.Bedroom4Upstairs);
                    cmd.Parameters.AddWithValue("@Bedroom4Downstairs", bedrooms.Bedroom4Downstairs);
                    cmd.Parameters.AddWithValue("@Bedroom4Comments", bedrooms.Bedroom4Comments ?? "");

                    cmd.Parameters.AddWithValue("@Bedroom5Tiled", bedrooms.Bedroom5Tiled);
                    cmd.Parameters.AddWithValue("@Bedroom5Wood", bedrooms.Bedroom5Wood);
                    cmd.Parameters.AddWithValue("@Bedroom5Carpet", bedrooms.Bedroom5Carpet);
                    cmd.Parameters.AddWithValue("@Bedroom5UnderFloorHeating", bedrooms.Bedroom5UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@Bedroom5AirConditioning", bedrooms.Bedroom5AirConditioning);
                    cmd.Parameters.AddWithValue("@Bedroom5Fan", bedrooms.Bedroom5Fan);
                    cmd.Parameters.AddWithValue("@Bedroom5BuiltInWardrobe", bedrooms.Bedroom5BuiltInWardrobe);
                    cmd.Parameters.AddWithValue("@Bedroom5WalkInCloset", bedrooms.Bedroom5WalkInCloset);
                    cmd.Parameters.AddWithValue("@Bedroom5EnSuite", bedrooms.Bedroom5EnSuite);
                    cmd.Parameters.AddWithValue("@Bedroom5SurroundSound", bedrooms.Bedroom5SurroundSound);
                    cmd.Parameters.AddWithValue("@Bedroom5FirePlaceGas", bedrooms.Bedroom5FirePlaceGas);
                    cmd.Parameters.AddWithValue("@Bedroom5FirePlaceWood", bedrooms.Bedroom5FirePlaceWood);
                    cmd.Parameters.AddWithValue("@Bedroom5Upstairs", bedrooms.Bedroom5Upstairs);
                    cmd.Parameters.AddWithValue("@Bedroom5Downstairs", bedrooms.Bedroom5Downstairs);
                    cmd.Parameters.AddWithValue("@Bedroom5Comments", bedrooms.Bedroom5Comments ?? "");

                    cmd.Parameters.AddWithValue("@Bedroom6Tiled", bedrooms.Bedroom6Tiled);
                    cmd.Parameters.AddWithValue("@Bedroom6Wood", bedrooms.Bedroom6Wood);
                    cmd.Parameters.AddWithValue("@Bedroom6Carpet", bedrooms.Bedroom6Carpet);
                    cmd.Parameters.AddWithValue("@Bedroom6UnderFloorHeating", bedrooms.Bedroom6UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@Bedroom6AirConditioning", bedrooms.Bedroom6AirConditioning);
                    cmd.Parameters.AddWithValue("@Bedroom6Fan", bedrooms.Bedroom6Fan);
                    cmd.Parameters.AddWithValue("@Bedroom6BuiltInWardrobe", bedrooms.Bedroom6BuiltInWardrobe);
                    cmd.Parameters.AddWithValue("@Bedroom6WalkInCloset", bedrooms.Bedroom6WalkInCloset);
                    cmd.Parameters.AddWithValue("@Bedroom6EnSuite", bedrooms.Bedroom6EnSuite);
                    cmd.Parameters.AddWithValue("@Bedroom6SurroundSound", bedrooms.Bedroom6SurroundSound);
                    cmd.Parameters.AddWithValue("@Bedroom6FirePlaceGas", bedrooms.Bedroom6FirePlaceGas);
                    cmd.Parameters.AddWithValue("@Bedroom6FirePlaceWood", bedrooms.Bedroom6FirePlaceWood);
                    cmd.Parameters.AddWithValue("@Bedroom6Upstairs", bedrooms.Bedroom6Upstairs);
                    cmd.Parameters.AddWithValue("@Bedroom6Downstairs", bedrooms.Bedroom6Downstairs);
                    cmd.Parameters.AddWithValue("@Bedroom6Comments", bedrooms.Bedroom6Comments ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertBathroomsInformation(Bathrooms bathrooms)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Bathroom_Information @LisingID,

	                                    @BathroomMainToilet,
	                                    @BathroomMainHandWashBasin,
	                                    @BathroomMainBath,
	                                    @BathroomMainShower,
	                                    @BathroomMainBidet,
	                                    @BathroomMainUnderFloorHeating,
	                                    @BathroomMainHeatingSystem,
	                                    @BathroomMainCloset,
	                                    @BathroomMainHeatedTowelRail,
	                                    @BathroomMainUpstairs,
	                                    @BathroomMainDownstairs,
	                                    @BathroomMainComments,

	                                    @Bathroom2Toilet,
	                                    @Bathroom2HandWashBasin,
	                                    @Bathroom2Bath,
	                                    @Bathroom2Shower,
	                                    @Bathroom2Bidet,
	                                    @Bathroom2UnderFloorHeating,
	                                    @Bathroom2HeatingSystem,
	                                    @Bathroom2Closet,
	                                    @Bathroom2HeatedTowelRail,
	                                    @Bathroom2Upstairs,
	                                    @Bathroom2Downstairs,
	                                    @Bathroom2Comments,

	                                    @Bathroom3Toilet,
	                                    @Bathroom3HandWashBasin,
	                                    @Bathroom3Bath,
	                                    @Bathroom3Shower,
	                                    @Bathroom3Bidet,
	                                    @Bathroom3UnderFloorHeating,
	                                    @Bathroom3HeatingSystem,
	                                    @Bathroom3Closet,
	                                    @Bathroom3HeatedTowelRail,
	                                    @Bathroom3Upstairs,
	                                    @Bathroom3Downstairs,
	                                    @Bathroom3Comments,

	                                    @Bathroom4Toilet,
	                                    @Bathroom4HandWashBasin,
	                                    @Bathroom4Bath,
	                                    @Bathroom4Shower,
	                                    @Bathroom4Bidet,
	                                    @Bathroom4UnderFloorHeating,
	                                    @Bathroom4HeatingSystem,
	                                    @Bathroom4Closet,
	                                    @Bathroom4HeatedTowelRail,
	                                    @Bathroom4Upstairs,
	                                    @Bathroom4Downstairs,
	                                    @Bathroom4Comments,

	                                    @Bathroom5Toilet,
	                                    @Bathroom5HandWashBasin,
	                                    @Bathroom5Bath,
	                                    @Bathroom5Shower,
	                                    @Bathroom5Bidet,
	                                    @Bathroom5UnderFloorHeating,
	                                    @Bathroom5HeatingSystem,
	                                    @Bathroom5Closet,
	                                    @Bathroom5HeatedTowelRail,
	                                    @Bathroom5Upstairs,
	                                    @Bathroom5Downstairs,
	                                    @Bathroom5Comments,

	                                    @BathroomGuestToilet,
	                                    @BathroomGuestHandWashBasin,
	                                    @BathroomGuestUnderFloorHeating,
	                                    @BathroomGuestHeatingSystem,
	                                    @BathroomGuestHeatedTowelRail,
	                                    @BathroomGuestUpstairs,
	                                    @BathroomGuestDownstairs,
	                                    @BathroomGuestComments";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@LisingID", bathrooms.ListingId);
                    cmd.Parameters.AddWithValue("@BathroomMainToilet", bathrooms.BathroomMainToilet);
                    cmd.Parameters.AddWithValue("@BathroomMainHandWashBasin", bathrooms.BathroomMainHandWashBasin);
                    cmd.Parameters.AddWithValue("@BathroomMainBath", bathrooms.BathroomMainBath);
                    cmd.Parameters.AddWithValue("@BathroomMainShower", bathrooms.BathroomMainShower);
                    cmd.Parameters.AddWithValue("@BathroomMainBidet", bathrooms.BathroomMainBidet);
                    cmd.Parameters.AddWithValue("@BathroomMainUnderFloorHeating", bathrooms.BathroomMainUnderFloorHeating);
                    cmd.Parameters.AddWithValue("@BathroomMainHeatingSystem", bathrooms.BathroomMainHeatingSystem);
                    cmd.Parameters.AddWithValue("@BathroomMainCloset", bathrooms.BathroomMainCloset);
                    cmd.Parameters.AddWithValue("@BathroomMainHeatedTowelRail", bathrooms.BathroomMainHeatedTowelRail);
                    cmd.Parameters.AddWithValue("@BathroomMainUpstairs", bathrooms.BathroomMainUpstairs);
                    cmd.Parameters.AddWithValue("@BathroomMainDownstairs", bathrooms.BathroomMainDownstairs);
                    cmd.Parameters.AddWithValue("@BathroomMainComments", bathrooms.BathroomMainComments ?? "");

                    cmd.Parameters.AddWithValue("@Bathroom2Toilet", bathrooms.Bathroom2Toilet);
                    cmd.Parameters.AddWithValue("@Bathroom2HandWashBasin", bathrooms.Bathroom2HandWashBasin);
                    cmd.Parameters.AddWithValue("@Bathroom2Bath", bathrooms.Bathroom2Bath);
                    cmd.Parameters.AddWithValue("@Bathroom2Shower", bathrooms.Bathroom2Shower);
                    cmd.Parameters.AddWithValue("@Bathroom2Bidet", bathrooms.Bathroom2Bidet);
                    cmd.Parameters.AddWithValue("@Bathroom2UnderFloorHeating", bathrooms.Bathroom2UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@Bathroom2HeatingSystem", bathrooms.Bathroom2HeatingSystem);
                    cmd.Parameters.AddWithValue("@Bathroom2Closet", bathrooms.Bathroom2Closet);
                    cmd.Parameters.AddWithValue("@Bathroom2HeatedTowelRail", bathrooms.Bathroom2HeatedTowelRail);
                    cmd.Parameters.AddWithValue("@Bathroom2Upstairs", bathrooms.Bathroom2Upstairs);
                    cmd.Parameters.AddWithValue("@Bathroom2Downstairs", bathrooms.Bathroom2Downstairs);
                    cmd.Parameters.AddWithValue("@Bathroom2Comments", bathrooms.Bathroom2Comments ?? "");

                    cmd.Parameters.AddWithValue("@Bathroom3Toilet", bathrooms.Bathroom3Toilet);
                    cmd.Parameters.AddWithValue("@Bathroom3HandWashBasin", bathrooms.Bathroom3HandWashBasin);
                    cmd.Parameters.AddWithValue("@Bathroom3Bath", bathrooms.Bathroom3Bath);
                    cmd.Parameters.AddWithValue("@Bathroom3Shower", bathrooms.Bathroom3Shower);
                    cmd.Parameters.AddWithValue("@Bathroom3Bidet", bathrooms.Bathroom3Bidet);
                    cmd.Parameters.AddWithValue("@Bathroom3UnderFloorHeating", bathrooms.Bathroom3UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@Bathroom3HeatingSystem", bathrooms.Bathroom3HeatingSystem);
                    cmd.Parameters.AddWithValue("@Bathroom3Closet", bathrooms.Bathroom3Closet);
                    cmd.Parameters.AddWithValue("@Bathroom3HeatedTowelRail", bathrooms.Bathroom3HeatedTowelRail);
                    cmd.Parameters.AddWithValue("@Bathroom3Upstairs", bathrooms.Bathroom3Upstairs);
                    cmd.Parameters.AddWithValue("@Bathroom3Downstairs", bathrooms.Bathroom3Downstairs);
                    cmd.Parameters.AddWithValue("@Bathroom3Comments", bathrooms.Bathroom3Comments ?? "");

                    cmd.Parameters.AddWithValue("@Bathroom4Toilet", bathrooms.Bathroom4Toilet);
                    cmd.Parameters.AddWithValue("@Bathroom4HandWashBasin", bathrooms.Bathroom4HandWashBasin);
                    cmd.Parameters.AddWithValue("@Bathroom4Bath", bathrooms.Bathroom4Bath);
                    cmd.Parameters.AddWithValue("@Bathroom4Shower", bathrooms.Bathroom4Shower);
                    cmd.Parameters.AddWithValue("@Bathroom4Bidet", bathrooms.Bathroom4Bidet);
                    cmd.Parameters.AddWithValue("@Bathroom4UnderFloorHeating", bathrooms.Bathroom4UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@Bathroom4HeatingSystem", bathrooms.Bathroom4HeatingSystem);
                    cmd.Parameters.AddWithValue("@Bathroom4Closet", bathrooms.Bathroom4Closet);
                    cmd.Parameters.AddWithValue("@Bathroom4HeatedTowelRail", bathrooms.Bathroom4HeatedTowelRail);
                    cmd.Parameters.AddWithValue("@Bathroom4Upstairs", bathrooms.Bathroom4Upstairs);
                    cmd.Parameters.AddWithValue("@Bathroom4Downstairs", bathrooms.Bathroom4Downstairs);
                    cmd.Parameters.AddWithValue("@Bathroom4Comments", bathrooms.Bathroom4Comments ?? "");

                    cmd.Parameters.AddWithValue("@Bathroom5Toilet", bathrooms.Bathroom5Toilet);
                    cmd.Parameters.AddWithValue("@Bathroom5HandWashBasin", bathrooms.Bathroom5HandWashBasin);
                    cmd.Parameters.AddWithValue("@Bathroom5Bath", bathrooms.Bathroom5Bath);
                    cmd.Parameters.AddWithValue("@Bathroom5Shower", bathrooms.Bathroom5Shower);
                    cmd.Parameters.AddWithValue("@Bathroom5Bidet", bathrooms.Bathroom5Bidet);
                    cmd.Parameters.AddWithValue("@Bathroom5UnderFloorHeating", bathrooms.Bathroom5UnderFloorHeating);
                    cmd.Parameters.AddWithValue("@Bathroom5HeatingSystem", bathrooms.Bathroom5HeatingSystem);
                    cmd.Parameters.AddWithValue("@Bathroom5Closet", bathrooms.Bathroom5Closet);
                    cmd.Parameters.AddWithValue("@Bathroom5HeatedTowelRail", bathrooms.Bathroom5HeatedTowelRail);
                    cmd.Parameters.AddWithValue("@Bathroom5Upstairs", bathrooms.Bathroom5Upstairs);
                    cmd.Parameters.AddWithValue("@Bathroom5Downstairs", bathrooms.Bathroom5Downstairs);
                    cmd.Parameters.AddWithValue("@Bathroom5Comments", bathrooms.Bathroom5Comments ?? "");

                    cmd.Parameters.AddWithValue("@BathroomGuestToilet", bathrooms.BathroomGuestToilet);
                    cmd.Parameters.AddWithValue("@BathroomGuestHandWashBasin", bathrooms.BathroomGuestHandWashBasin);
                    cmd.Parameters.AddWithValue("@BathroomGuestUnderFloorHeating", bathrooms.BathroomGuestUnderFloorHeating);
                    cmd.Parameters.AddWithValue("@BathroomGuestHeatingSystem", bathrooms.BathroomGuestHeatingSystem);
                    cmd.Parameters.AddWithValue("@BathroomGuestHeatedTowelRail", bathrooms.BathroomGuestHeatedTowelRail);
                    cmd.Parameters.AddWithValue("@BathroomGuestUpstairs", bathrooms.BathroomGuestUpstairs);
                    cmd.Parameters.AddWithValue("@BathroomGuestDownstairs", bathrooms.BathroomGuestDownstairs);
                    cmd.Parameters.AddWithValue("@BathroomGuestComments", bathrooms.BathroomGuestComments ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertExclusionsInclusions(ExclusionsInclusions exclusionsInclusions)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Exclusions_Inclusions @ListingId,
	                                    @ExclusionPlants,
	                                    @ExclusionGardenFeatures,
	                                    @ExclusionBlinds,
	                                    @ExclusionCurtains,
	                                    @ExclusionSatelliteDish,
	                                    @ExclusionWendyHouse,
	                                    @ExclusionGardenShed,
	                                    @ExclusionSafe,
                                        @InclusionPlants,
                                        @InclusionGardenFeatures,
                                        @InclusionBlinds,
                                        @InclusionCurtains,
                                        @InclusionSatelliteDish,
                                        @InclusionWendyHouse,
                                        @InclusionGardenShed,
                                        @InclusionSafe,
                                        @ExclusionInclusionComments";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingId", exclusionsInclusions.ListingId);
                    cmd.Parameters.AddWithValue("@ExclusionPlants", exclusionsInclusions.ExclusionPlants);
                    cmd.Parameters.AddWithValue("@ExclusionGardenFeatures", exclusionsInclusions.ExclusionGardenFeatures);
                    cmd.Parameters.AddWithValue("@ExclusionBlinds", exclusionsInclusions.ExclusionBlinds);
                    cmd.Parameters.AddWithValue("@ExclusionCurtains", exclusionsInclusions.ExclusionCurtains);
                    cmd.Parameters.AddWithValue("@ExclusionSatelliteDish", exclusionsInclusions.ExclusionSatelliteDish);
                    cmd.Parameters.AddWithValue("@ExclusionWendyHouse", exclusionsInclusions.ExclusionWendyHouse);
                    cmd.Parameters.AddWithValue("@ExclusionGardenShed", exclusionsInclusions.ExclusionGardenShed);
                    cmd.Parameters.AddWithValue("@ExclusionSafe", exclusionsInclusions.ExclusionSafe);
                    cmd.Parameters.AddWithValue("@InclusionPlants", exclusionsInclusions.InclusionPlants);
                    cmd.Parameters.AddWithValue("@InclusionGardenFeatures", exclusionsInclusions.InclusionGardenFeatures);
                    cmd.Parameters.AddWithValue("@InclusionBlinds", exclusionsInclusions.InclusionBlinds);
                    cmd.Parameters.AddWithValue("@InclusionCurtains", exclusionsInclusions.InclusionCurtains);
                    cmd.Parameters.AddWithValue("@InclusionSatelliteDish", exclusionsInclusions.InclusionSatelliteDish);
                    cmd.Parameters.AddWithValue("@InclusionWendyHouse", exclusionsInclusions.InclusionWendyHouse);
                    cmd.Parameters.AddWithValue("@InclusionGardenShed", exclusionsInclusions.InclusionGardenShed);
                    cmd.Parameters.AddWithValue("@InclusionSafe", exclusionsInclusions.InclusionSafe);
                    cmd.Parameters.AddWithValue("@ExclusionInclusionComments", exclusionsInclusions.ExclusionInclusionComments ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertSummary(Summary summary)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_Summary @ListingId, @Bedrooms, @Study, @StudyNook, @Bathrooms, @LivingAreas, @LUGarages, @CoveredBays, @OpenBays";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingId", summary.ListingId);
                    cmd.Parameters.AddWithValue("@Bedrooms", summary.Bedrooms);
                    cmd.Parameters.AddWithValue("@Study", summary.Study);
                    cmd.Parameters.AddWithValue("@StudyNook", summary.StudyNook);
                    cmd.Parameters.AddWithValue("@Bathrooms", summary.Bathrooms);
                    cmd.Parameters.AddWithValue("@LivingAreas", summary.LivingAreas);
                    cmd.Parameters.AddWithValue("@LUGarages", summary.LUGarages);
                    cmd.Parameters.AddWithValue("@CoveredBays", summary.CoveredBays);
                    cmd.Parameters.AddWithValue("@OpenBays", summary.OpenBays);

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void InsertHistory(History history)
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"EXEC fa_Insert_History @ListingId, @DateSold, @PriceSold, @SoldComments, @DateRented, @RentalGrossAmount, 
                                        @RentalComments, @WidthdrawnComments";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@ListingId", history.ListingId);
                    cmd.Parameters.AddWithValue("@DateSold", history.DateSold ?? "");
                    cmd.Parameters.AddWithValue("@PriceSold", history.PriceSold);
                    cmd.Parameters.AddWithValue("@SoldComments", history.SoldComments ?? "");
                    cmd.Parameters.AddWithValue("@DateRented", history.DateRented ?? "");
                    cmd.Parameters.AddWithValue("@RentalGrossAmount", history.RentalGrossAmount);
                    cmd.Parameters.AddWithValue("@RentalComments", history.RentalComments ?? "");
                    cmd.Parameters.AddWithValue("@WidthdrawnComments", history.WidthdrawnComments ?? "");

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public int GetListingID()
        {
            using (SqlConnection cn = new SqlConnection(ConnStr))
            {
                cn.Open();

                string sqlQuery = @"SELECT Max(Id) AS Id FROM Listing";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            return Convert.ToInt32(rd["Id"]);
                        }
                    }
                }
            }
            return 0;
        }
    }
}
