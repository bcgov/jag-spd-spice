using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gov.Jag.Spice.Interfaces.SharePoint;

public class FolderItem
{
    public string Name { get; set; }
    public string ServerRelativeUrl { get; set; }
    public DateTime TimeCreated { get; set; }
    public DateTime TimeLastModified { get; set; }
}

public class FileItem
{
    public string Name { get; set; }
    public string ServerRelativeUrl { get; set; }
    public long Length { get; set; }
    public DateTime TimeCreated { get; set; }
    public DateTime TimeLastModified { get; set; }
}

public class FolderSegment
{
    public string FolderNameSegment { get; set; }
    public string FolderGuidSegment { get; set; }
    public string FolderName { get; set; }
}

public class EnsureFolderPathRequest
{
    public string EntityName { get; set; }
    public List<FolderSegment> FolderPath { get; set; }
}

public class SharePointFileDetailsList
{
    public string Name { get; set; }
    public string TimeLastModified { get; set; }
    public string TimeCreated { get; set; }
    public string Length { get; set; }
    public string DocumentType { get; set; }
    public string ServerRelativeUrl { get; set; }
}

public class FileSystemItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Documenttype { get; set; }
    public int Size { get; set; }
    public string Serverrelativeurl { get; set; }
    public DateTime Timecreated { get; set; }
    public DateTime Timelastmodified { get; set; }
}

class DocumentLibraryResponse
{
    public DocumentLibraryResponseContent d { get; set; }
}

class DocumentLibraryResponseContent
{
    public string Id { get; set; }
}

