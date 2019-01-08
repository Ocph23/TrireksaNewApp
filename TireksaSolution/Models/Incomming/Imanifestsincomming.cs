using System;
 
 namespace Incomming 
{ 

     public interface Imanifestsincomming  
   {
         int Id {  get; set;} 

         string ManifestNumber {  get; set;} 

         int AgentId {  get; set;} 

         string Via {  get; set;} 

         DateTime CreateDate {  get; set;} 

         string UserId {  get; set;} 

         string UpdateDate {  get; set;} 

     }
}


