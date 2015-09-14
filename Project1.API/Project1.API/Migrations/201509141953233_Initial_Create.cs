namespace Project1.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "USER.AccessToken",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        AccessToken = c.String(maxLength: 1000),
                        ExpiresOn = c.DateTime(nullable: false),
                        UpdatedTS = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("USER.ApplicationUser", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "USER.ApplicationUser",
                c => new
                    {
                        UserID = c.Guid(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                        SecurityHash = c.String(maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 16),
                        Password = c.String(nullable: false, maxLength: 100),
                        IsPhoneConfirmed = c.Boolean(nullable: false),
                        JoinTS = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "DOC.Document",
                c => new
                    {
                        DocumentID = c.Guid(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 200),
                        FileType = c.Int(nullable: false),
                        UploadedTS = c.DateTime(nullable: false),
                        LastUpdatedTS = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.DocumentID)
                .ForeignKey("USER.ApplicationUser", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "USER.Share",
                c => new
                    {
                        ShareID = c.Guid(nullable: false, identity: true),
                        DocumentID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        SharedTS = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ShareID)
                .ForeignKey("USER.ApplicationUser", t => t.UserID)
                .ForeignKey("DOC.Document", t => t.DocumentID)
                .Index(t => t.DocumentID)
                .Index(t => t.UserID);
            
            CreateTable(
                "USER.Changeset",
                c => new
                    {
                        ChangesetID = c.Guid(nullable: false, identity: true),
                        ChangesetNumber = c.Int(nullable: false, identity: true),
                        ShareID = c.Guid(nullable: false),
                        PushedAt = c.DateTime(nullable: false),
                        PulledAt = c.DateTime(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.ChangesetID, t.ChangesetNumber })
                .ForeignKey("USER.Share", t => t.ShareID)
                .Index(t => t.ShareID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("USER.AccessToken", "UserID", "USER.ApplicationUser");
            DropForeignKey("USER.Share", "DocumentID", "DOC.Document");
            DropForeignKey("USER.Changeset", "ShareID", "USER.Share");
            DropForeignKey("USER.Share", "UserID", "USER.ApplicationUser");
            DropForeignKey("DOC.Document", "UserID", "USER.ApplicationUser");
            DropIndex("USER.Changeset", new[] { "ShareID" });
            DropIndex("USER.Share", new[] { "UserID" });
            DropIndex("USER.Share", new[] { "DocumentID" });
            DropIndex("DOC.Document", new[] { "UserID" });
            DropIndex("USER.AccessToken", new[] { "UserID" });
            DropTable("USER.Changeset");
            DropTable("USER.Share");
            DropTable("DOC.Document");
            DropTable("USER.ApplicationUser");
            DropTable("USER.AccessToken");
        }
    }
}
