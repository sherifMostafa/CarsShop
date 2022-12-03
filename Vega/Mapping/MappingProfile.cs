using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.Domains;
using Vega.Resources;

namespace Vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain To Api Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>() ;

            CreateMap<Feature, KeyValuePairResource>();

            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(v => v.Contact , opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName  , Email = v.ContactEmail , Phone = v.ContactPhone}))
                .ForMember(v => v.Features , opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));


            CreateMap<Vehicle , VehicleResource>()
                .ForMember(vr => vr.Make , opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(v => v.Contact , opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName  , Email = v.ContactEmail , Phone = v.ContactPhone}))
                .ForMember(vr => vr.Features , opt => opt.MapFrom(v => v.Features.Select(v => new KeyValuePairResource{ Id=v.Feature.Id , Name=v.Feature.Name})));


            //Api Resource To Domain 
            CreateMap<VehicleQueryResource, VehicleQuery>();

            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) => {

                    //Remove UnSelected Features
                    //var removedFeatures = new List<VehicleFeature>();
                    //foreach (var f in v.Features)
                    //    if (!vr.Features.Contains(f.FeatureId))
                    //        removedFeatures.Add(f);
                    //foreach (var f in removedFeatures)
                    //    v.Features.Remove(f);
                    //Add new Features
                    //foreach (var id in vr.Features)
                    //    if (!v.Features.Any(f => f.FeatureId == id))
                    //        v.Features.Add(new VehicleFeature { FeatureId = id });
                    //-------------------------------------------------------------------
                    //Remove UnSelected Features
                    var rermovedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));
                    foreach (var f in rermovedFeatures)
                        v.Features.Remove(f);


                    //Add New Features 
                    var addedFeatures =  vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id))
                    .Select(id => new VehicleFeature { FeatureId = id });
                    foreach (var item in addedFeatures)
                        v.Features.Add(item);

                
                });
                //.ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature { FeatureId = id }))); 
        }       
    }
}
