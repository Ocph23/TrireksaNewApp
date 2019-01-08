using System;
 
 namespace Incomming 
{ 

     public interface Ishipinginformation  
   {
         int ManifestId {  get; set;} 

         string ArmadaName {  get; set;} 

         string ArmadaNumber {  get; set;} 

         int Package {  get; set;} 

         DateTime OnOriginPort {  get; set;} 

         DateTime OnDestinationPort {  get; set;} 

         int OriginId {  get; set;} 

         int DestinationId {  get; set;} 

     }
}


