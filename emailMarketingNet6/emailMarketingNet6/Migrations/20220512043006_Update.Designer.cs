﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using emailMarketingNet6.Models;

#nullable disable

namespace emailMarketingNet6.Migrations
{
    [DbContext(typeof(EmailMarketingContext))]
    [Migration("20220512043006_Update")]
    partial class Update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("emailMarketingNet6.Models.CampaignModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Actived")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ContactListId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Day")
                        .HasColumnType("TEXT");

                    b.Property<int>("EmailSendId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EmailSenderId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Hour")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Minute")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Schedule")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TemplateId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ContactListId");

                    b.HasIndex("EmailSenderId");

                    b.HasIndex("TemplateId");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("emailMarketingNet6.Models.ContactListModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ContactLists");
                });

            modelBuilder.Entity("emailMarketingNet6.Models.ContactModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ContactListId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContactListId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("emailMarketingNet6.Models.EmailModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PassLogin")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("emailMarketingNet6.Models.TemplateModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("emailMarketingNet6.Models.CampaignModel", b =>
                {
                    b.HasOne("emailMarketingNet6.Models.ContactListModel", "ContactList")
                        .WithMany("Campaigns")
                        .HasForeignKey("ContactListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("emailMarketingNet6.Models.EmailModel", "EmailSender")
                        .WithMany("Campaigns")
                        .HasForeignKey("EmailSenderId");

                    b.HasOne("emailMarketingNet6.Models.TemplateModel", "Template")
                        .WithMany("Campaigns")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactList");

                    b.Navigation("EmailSender");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("emailMarketingNet6.Models.ContactModel", b =>
                {
                    b.HasOne("emailMarketingNet6.Models.ContactListModel", "ContactList")
                        .WithMany("Contacts")
                        .HasForeignKey("ContactListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactList");
                });

            modelBuilder.Entity("emailMarketingNet6.Models.ContactListModel", b =>
                {
                    b.Navigation("Campaigns");

                    b.Navigation("Contacts");
                });

            modelBuilder.Entity("emailMarketingNet6.Models.EmailModel", b =>
                {
                    b.Navigation("Campaigns");
                });

            modelBuilder.Entity("emailMarketingNet6.Models.TemplateModel", b =>
                {
                    b.Navigation("Campaigns");
                });
#pragma warning restore 612, 618
        }
    }
}
